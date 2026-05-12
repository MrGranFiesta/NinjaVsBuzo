using System;
using System.Linq;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Bat : MonoBehaviourPun
{
    private float distancia;

    [NonSerialized] public Vector3 PointInital;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        PointInital = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        
        distancia = GetDistancePlayerNearby();
        animator.SetFloat("Distancia", distancia);
    }

    [PunRPC]
    public void FlipX(Vector3 objetivo)
    {
        if (transform.position.x < objetivo.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private float GetDistancePlayerNearby()
    {
        Transform playerNearby = GetPlayerNearby();

        if (playerNearby == null)
        {
            return float.MaxValue;
        }

        return Vector2.Distance(transform.position, playerNearby.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(TagsUtils.IsPlayer(collision.gameObject))
        {
            MainClass.GameManager.MinusScore();
            collision.gameObject
                .GetComponent<PlayerController>()
                ?.ApplyInvulnerability();
        }
    }

    public Transform GetPlayerNearby()     {
        if (SceneGameManager.Players == null || SceneGameManager.Players.Count == 0)
        {
            return null;
        }
        return SceneGameManager.Players.OrderBy(j => Vector2.Distance(transform.position, j.position))
            .FirstOrDefault();
    }
}
