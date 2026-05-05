using UnityEngine;
using Photon.Pun;

public class Fruit : MonoBehaviourPun
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TagsUtils.IsPlayer(collision.gameObject))
        {
            // Reproducimos el sonido localmente (cada cliente lo oye en su propia máquina)
            SoundConst.EatFruit.Play();

            // Solo el MasterClient puede destruir objetos de red.
            // Enviamos un RPC al MasterClient para que destruya la fruta en todos los clientes.
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
