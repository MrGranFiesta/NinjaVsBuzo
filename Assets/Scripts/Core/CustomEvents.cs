using UnityEngine;
using UnityEngine.Events;

public class CustomEvents
{
    public UnityEvent<Transform> OnRegistryPlayer = new UnityEvent<Transform>();
    public UnityEvent OnMineScoreChanged = new UnityEvent();
}
