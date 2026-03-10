using Photon.Pun;
using UnityEngine;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView pv;

    private void Awake()
    {
        Debug.Log("PlayerManager - Awake");
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        Debug.Log("PlayerManager - Start");
        if (pv.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        Debug.Log("PlayerManager - CreateController");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
    }
}
