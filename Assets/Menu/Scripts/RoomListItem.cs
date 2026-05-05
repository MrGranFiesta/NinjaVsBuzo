using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text label;

    RoomInfo info;
    public void SetUp(RoomInfo _info)
    {
        Debug.Log("RoomListItem - SetUp");
        info = _info;
        label.text = _info.Name;
    }

    public void OnClick()
    {
        Debug.Log("RoomListItem - OnClick");
        Launcher.Instance.JoinRoom(info);
    }
}
