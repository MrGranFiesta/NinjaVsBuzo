using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldSettings", menuName = "Procedural/WorldSettings")]
public class WorldSettings : ScriptableObject
{
    [Header("Map Dimensions")]
    public int mapWidth = 50;
    public int mapHeight = 30;
    
    [Header("Platform Settings")]
    public GameObject platformPrefab;
    public GameObject wallPrefab;
    [Range(0, 1)] public float platformDensity = 0.2f;
    public int minPlatformWidth = 3;
    public int maxPlatformWidth = 7;
    
    [Header("Enemy Settings")]
    public List<SpawnableEntity> enemies;
    public int maxEnemies = 5;

    [Header("Fruit Settings")]
    public List<SpawnableEntity> fruits;
    public int maxFruits = 10;
}

[Serializable]
public class SpawnableEntity
{
    public string prefabName; // Nombre en Resources
    public GameObject prefab;  // Opcional si se quiere asignar directamente
    [Range(0, 1)] public float spawnProbability = 0.5f;
}
