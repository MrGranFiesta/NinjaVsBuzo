using UnityEngine;

public class MainClass
{
    public static AudioManager AudioManager;
    public static CustomEvents CustomEvents;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Start()
    {
        AudioManager = new AudioManager();
        CustomEvents = new CustomEvents();
    }
}
