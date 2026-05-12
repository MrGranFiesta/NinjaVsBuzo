using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Fruit", menuName = "Fruit/FruitManager")]
public class FruitManager : ScriptableObject
{
    public List<FruitType> fruitTypes;

    public FruitType GetRandomFruit()
    {
        if (fruitTypes == null || fruitTypes.Count == 0) return null;
        float totalProb = fruitTypes.Sum(f => f.spawnProbability);
        float randomPoint = Random.value * totalProb;
        float currentProbSum = 0;
        return fruitTypes
            .FirstOrDefault(f =>
            {
                currentProbSum += f.spawnProbability;
                return randomPoint <= currentProbSum;
            }) ?? fruitTypes[0];
    }

    public int GetIndexRandomFruit()
    {
        if (fruitTypes == null || fruitTypes.Count == 0) return 0;
        float totalProb = fruitTypes.Sum(f => f.spawnProbability);
        float randomPoint = Random.value * totalProb;
        float currentProbSum = 0;
        int index = 0;
        
        for(int i = 0; i < fruitTypes.Count; i++)
        {
            currentProbSum += fruitTypes[i].spawnProbability;

            if (randomPoint <= currentProbSum)
                return i;
        }

        return fruitTypes.Count - 1;
    }

    public FruitType GetFruitByIndex(int index)
    {
        if(fruitTypes == null || fruitTypes.Count == 0) return null;
        if (index < 0 || index >= fruitTypes.Count) return null;
        return fruitTypes[index];
    }
}
