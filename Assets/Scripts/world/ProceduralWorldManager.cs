using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using ExitGames.Client.Photon;

public class ProceduralWorldManager : MonoBehaviourPunCallbacks
{
    public WorldSettings settings;
    private const string SEED_KEY = "MapSeed";
    
    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int seed = Random.Range(0, 100000);
                Hashtable props = new Hashtable { { SEED_KEY, seed } };
                PhotonNetwork.CurrentRoom.SetCustomProperties(props);
                
                // Iniciar generación localmente inmediatamente para el Master
                GenerateWorld(seed);
            }
            else
            {
                // Los clientes esperan a que la propiedad esté disponible
                if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(SEED_KEY, out object seedValue))
                {
                    GenerateWorld((int)seedValue);
                }
            }
        }
        else
        {
            // MODO LOCAL: Para pruebas sin red
            Debug.Log("Iniciando en modo LOCAL");
            int localSeed = UnityEngine.Random.Range(0, 100000);
            GenerateWorld(localSeed);
            SpawnLocalPlayer();
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (!PhotonNetwork.IsMasterClient && propertiesThatChanged.ContainsKey(SEED_KEY))
        {
            GenerateWorld((int)propertiesThatChanged[SEED_KEY]);
        }
    }

    private void GenerateWorld(int seed)
    {
        Debug.Log("Generando mundo con semilla: " + seed);
        UnityEngine.Random.InitState(seed);
        
        WorldGenerator generator = new WorldGenerator(settings, transform);
        generator.Execute();
    }

    private void SpawnLocalPlayer()
    {
        // Cargamos el player de prueba (Frog) desde Resources
        GameObject playerPrefab = Resources.Load<GameObject>("PhotonPrefabs/Frog");
        if (playerPrefab != null)
        {
            // Spawn en el centro inferior del mapa
            Vector3 spawnPos = new Vector3(settings.mapWidth / 2, 2, 0);
            Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Jugador local instanciado para pruebas.");
        }
        else
        {
            Debug.LogError("No se encontró el prefab del jugador en Resources/PhotonPrefabs/Frog");
        }
    }
}
