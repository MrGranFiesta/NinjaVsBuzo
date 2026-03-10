public class SoundConst
{
    public string Value { get; }

    private SoundConst(string value)
    {
        Value = value;
    }

    public void Play()
    {
        MainClass.AudioManager.AudioPooling.PlaySound(ResourceManager.GetClip(this));
    }

    //Sound
    public static readonly SoundConst EatFruit = new SoundConst("eatFruit");
}
