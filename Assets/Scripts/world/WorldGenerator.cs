using UnityEngine;
using System.Collections.Generic;

public class WorldGenerator
{
    private WorldSettings settings;
    private Transform parent;
    private List<Vector2> platformSurfaces = new List<Vector2>();

    public WorldGenerator(WorldSettings settings, Transform parent)
    {
        this.settings = settings;
        this.parent = parent;
    }

    public void Execute()
    {
        CreateBoundaries();
        CreatePlatforms();
        SpawnEntities(settings.fruits, settings.maxFruits, "Fruit");
        SpawnEntities(settings.enemies, settings.maxEnemies, "Enemy");
    }

    private void CreateBoundaries()
    {
        float w = settings.mapWidth;
        float h = settings.mapHeight;

        // Suelo, Techo, Pared Izquierda, Pared Derecha
        SpawnWall(new Vector2(0, -1), new Vector2(w, 1)); // Suelo
        SpawnWall(new Vector2(0, h), new Vector2(w, 1));  // Techo
        SpawnWall(new Vector2(-1, h / 2), new Vector2(1, h + 2)); // Pared Izq
        SpawnWall(new Vector2(w, h / 2), new Vector2(1, h + 2));  // Pared Der
    }

    private void SpawnWall(Vector2 pos, Vector2 scale)
    {
        GameObject wall = Object.Instantiate(settings.wallPrefab, pos, Quaternion.identity, parent);
        wall.transform.localScale = scale;
    }

    private void CreatePlatforms()
    {
        // Algoritmo simple de plataformas aleatorias
        int attempts = (int)(settings.mapWidth * settings.mapHeight * settings.platformDensity / 5);
        
        for (int i = 0; i < attempts; i++)
        {
            float x = Random.Range(2, settings.mapWidth - 5);
            float y = Random.Range(2, settings.mapHeight - 5);
            int width = Random.Range(settings.minPlatformWidth, settings.maxPlatformWidth);

            Vector3 pos = new Vector3(x, y, 0);
            GameObject plat = Object.Instantiate(settings.platformPrefab, pos, Quaternion.identity, parent);
            plat.transform.localScale = new Vector3(width, 1, 1);

            // Guardar la superficie para spawnear cosas encima
            for (int j = 0; j < width; j++)
            {
                platformSurfaces.Add(new Vector2(x + (j - width/2f), y + 0.6f));
            }
        }
    }

    private void SpawnEntities(List<SpawnableEntity> entityList, int maxAmount, string folder)
    {
        if (platformSurfaces.Count == 0) return;

        int count = 0;
        int maxAttempts = maxAmount * 2;
        
        while (count < maxAmount && maxAttempts > 0)
        {
            maxAttempts--;
            Vector2 surface = platformSurfaces[Random.Range(0, platformSurfaces.Count)];
            
            // Elegir entidad basada en probabilidad
            SpawnableEntity entityToSpawn = GetRandomEntity(entityList);
            if (entityToSpawn != null)
            {
                // Cargar desde Resources
                GameObject prefab = Resources.Load<GameObject>($"{folder}/{entityToSpawn.prefabName}");
                if (prefab != null)
                {
                    Object.Instantiate(prefab, surface, Quaternion.identity);
                    count++;
                }
            }
        }
    }

    private SpawnableEntity GetRandomEntity(List<SpawnableEntity> list)
    {
        float totalProb = 0;
        foreach (var e in list) totalProb += e.spawnProbability;
        
        float randomPoint = Random.value * totalProb;
        float currentProb = 0;
        
        foreach (var e in list)
        {
            currentProb += e.spawnProbability;
            if (randomPoint <= currentProb) return e;
        }
        return null;
    }
}
