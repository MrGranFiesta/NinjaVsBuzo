using UnityEngine;

public class MainClass
{
    public static AudioManager AudioManager;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Start()
    {
        AudioManager = new AudioManager();
    }
}
