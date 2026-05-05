using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameSceneInitializer : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        // Si ya estamos conectados, este script no es necesario (ya vendría del flujo normal)
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("GameSceneInitializer: Ya conectado a Photon. Destruyendo objeto de depuración.");
            Destroy(gameObject);
            return;
        }

        Debug.Log("GameSceneInitializer: Modo Depuración Detectado. Conectando Offline...");
        
        // Configuramos Photon para modo offline
        PhotonNetwork.OfflineMode = true;
    }

    private void Start()
    {
        if (PhotonNetwork.OfflineMode)
        {
            Debug.Log("GameSceneInitializer: Creando sala de depuración...");
            PhotonNetwork.CreateRoom("DebugRoom");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"GameSceneInitializer: Error al crear sala: {message} ({returnCode})");
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.OfflineMode)
        {
            Debug.Log("GameSceneInitializer: Unido a sala Offline. Dejando que GameManager maneje la instanciación.");
        }
    }
}
