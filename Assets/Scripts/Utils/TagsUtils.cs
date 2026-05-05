using UnityEngine;

public class TagsUtils
{
    public const string MainCamera = "MainCamera";
    public const string Untagged = "Untagged";
    public const string Player = "Player";
    public const string EditorOnly = "EditorOnly";
    public const string GameController = "GameController";
    public const string Respawn = "Respawn";
    public const string Finish = "Finish";

    public static bool IsPlayer(GameObject go)
    {
        return go.transform.CompareTag(Player);
    }

}
