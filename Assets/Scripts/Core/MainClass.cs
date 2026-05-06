using UnityEngine;
using Photon.Pun;

public class MainClass
{
    public static AudioManager AudioManager;
    public static CustomEvents CustomEvents;
    public static GameManager GameManager;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Start()
    {
        AudioManager = new AudioManager();
        CustomEvents = new CustomEvents();
        GameManager = new GameManager();
    }
}
