using UnityEngine;

[System.Serializable]
public class FruitType
{
    public int points = 1;
    [Range(0, 100)] public float spawnProbability = 10f;
    public RuntimeAnimatorController animatorController;
    public Sprite sprite;
}
