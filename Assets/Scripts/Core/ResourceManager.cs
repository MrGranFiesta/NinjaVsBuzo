using UnityEngine;

public class ResourceManager
{

    public static AudioClip GetClip(SoundConst audio)
    {
        return Resources.Load<AudioClip>($"Audio/{audio.Value}");
    }
}
