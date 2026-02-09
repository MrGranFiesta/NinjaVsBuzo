using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Connection : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        print("Conectado al master !!!!");
    }

    public void ButtonConnect()
    {
        RoomOptions options = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom("room1", options, TypedLobby.Default);
    }

   
    public override void OnJoinedRoom()
    {
        Debug.Log("Conectada a la sala " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Hay..." + PhotonNetwork.CurrentRoom.PlayerCount + " jugadores");
    }
    
    private void Update()
    {
        if (PhotonNetwork.IsMasterClient) { 
            Debug.Log($"PlayerCount {PhotonNetwork.CurrentRoom.PlayerCount}");
        }
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            PhotonNetwork.LoadLevel(1);
            Destroy(this);
        }
    }
}
