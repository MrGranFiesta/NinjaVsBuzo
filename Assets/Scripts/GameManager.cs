using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Frog", new Vector3(-2, -3, 0), Quaternion.identity);
        }
        else {
            PhotonNetwork.Instantiate("Virtual", new Vector3(-3, -3, 0), Quaternion.identity);
        }
    }
}
