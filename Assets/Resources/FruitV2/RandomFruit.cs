using Photon.Pun;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonAnimatorView))]
public class RandomFruit : MonoBehaviourPun
{
    [Header("Configuration")]
    [SerializeField] private FruitManager fruitManager;
    private float minRespawnTime = 10f;
    private float maxRespawnTime = 15f;


    private SpriteRenderer sprite;
    private Animator animator;
    private Collider2D col;
    private FruitType currentFruit;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnected)
        {
            PickRandomFruit();
        }
    }

    private void PickRandomFruit()
    {        
        int index = fruitManager.GetIndexRandomFruit();

        if (PhotonNetwork.InRoom && photonView != null && photonView.ViewID > 0)
        {
            photonView.RPC("SetFruitAppearance", RpcTarget.AllBuffered, index);
        }
        else
        {
            // Si no hay red o el objeto no tiene ID de red, lo aplicamos localmente
            if (PhotonNetwork.InRoom) Debug.LogWarning($"RandomFruit: Intentando RPC en objeto sin ViewID válido (ID: {photonView?.ViewID}). Aplicando local.");
            SetFruitAppearance(index);
        }
    }

    [PunRPC]
    private void SetFruitAppearance(int index)
    {
        currentFruit = fruitManager.GetFruitByIndex(index);
        animator.runtimeAnimatorController = currentFruit.animatorController;
        sprite.sprite = currentFruit.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagsUtils.IsPlayer(collision.gameObject))
        {
            Collect(collision.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        if (!sprite.enabled) return;

        PhotonView playerPV = player.GetComponent<PhotonView>();
        if (playerPV != null && (playerPV.IsMine || !PhotonNetwork.IsConnected))
        {
            MainClass.GameManager.AddPoints(currentFruit.points);
        }

        SoundConst.EatFruit.Play();

        if (PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnected)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    private IEnumerator RespawnRoutine()
    {
        // Ocultar en todos los clientes
        SyncVisibilityAll(false);

        float waitTime = Random.Range(minRespawnTime, maxRespawnTime);
        yield return new WaitForSeconds(waitTime);

        PickRandomFruit();

        SyncVisibilityAll(true);
    }

    private void SyncVisibilityAll(bool isVisible)
    {
        if (PhotonNetwork.IsConnected) { 
            Debug.Log("AAA1: " + isVisible);
            photonView.RPC("SyncVisibility", RpcTarget.AllBuffered, isVisible);
        }
         else
        {
            Debug.Log("AAA2: " + isVisible);
            SyncVisibility(isVisible);
        }
            
    }

    [PunRPC]
    private void SyncVisibility(bool isVisible)
    {
        sprite.enabled = isVisible;
        col.enabled = isVisible;
    }
}
