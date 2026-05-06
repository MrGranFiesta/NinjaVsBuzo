using UnityEngine;
using Photon.Pun;

public class Fruit : MonoBehaviourPun
{
    [SerializeField] private int points = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TagsUtils.IsPlayer(collision.gameObject))
        {
            PhotonView playerPV = collision.gameObject.GetComponent<PhotonView>();
            if (playerPV != null && playerPV.IsMine)
            {
                // Solo sumamos puntos si somos nosotros los que tocamos la fruta
                MainClass.GameManager.AddPoints(points);
            }

            // Reproducimos el sonido localmente
            SoundConst.EatFruit.Play();

            // Solo el MasterClient puede destruir objetos de red.
            photonView.RPC("DestroyFruit", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    private void DestroyFruit()
    {
        // PhotonNetwork.Destroy propaga la destrucción a todos los clientes conectados.
        PhotonNetwork.Destroy(gameObject);
    }
}
