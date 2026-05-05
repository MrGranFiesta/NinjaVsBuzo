using Photon.Pun;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    private bool playerSpawned = false;
    [SerializeField] private Transform[] SpawnPoint;

    private void Awake()
    {
        if (SpawnPoint == null || SpawnPoint.Length != 2 || SpawnPoint.Any( i => i == null))
        {
            SpawnPoint = new Transform[2];
            Transform spawnPoint1 = new GameObject("SpawnPoint1").transform;
            spawnPoint1.position = new Vector3(-1.6f, -3.5f, 0);
            SpawnPoint[0] = spawnPoint1;
            Transform spawnPoint2 = new GameObject("SpawnPoint2").transform;
            spawnPoint2.position = new Vector3(2, -3.5f, 0);
            SpawnPoint[1] = spawnPoint2;
        }    
    }

    void Start()
    {
        //Debug.Log($"GameManager: Start. InRoom: {PhotonNetwork.InRoom}");
        // Si ya estamos en una sala (flujo normal viniendo desde el menú), instanciamos inmediatamente
        if (PhotonNetwork.InRoom)
        {
            SpawnPlayer();
        }
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log("GameManager: OnJoinedRoom llamado.");
        // Si acabamos de unirnos a la sala (flujo de depuración), instanciamos al jugador
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (playerSpawned)
        {
            //Debug.Log("GameManager: El jugador ya ha sido instanciado. Ignorando.");
            return;
        }

        playerSpawned = true;
        //Debug.Log($"GameManager: Instanciando jugador. Master: {PhotonNetwork.IsMasterClient}");

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("PhotonPrefabs/Frog", SpawnPoint[0].position, Quaternion.identity);
        }
        else 
        {
            PhotonNetwork.Instantiate("PhotonPrefabs/Virtual", SpawnPoint[1].position, Quaternion.identity);
        }
    }
}
