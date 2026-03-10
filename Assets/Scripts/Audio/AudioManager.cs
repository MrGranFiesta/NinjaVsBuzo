using UnityEngine;

public class AudioManager
{
    private GameObject _audioPoolingGO;
    public AudioPooling AudioPooling;

    public AudioManager()
    {
        _audioPoolingGO = new GameObject("AudioPooling");
        Object.DontDestroyOnLoad(_audioPoolingGO);
        AudioPooling = new AudioPooling(_audioPoolingGO);
    }
}