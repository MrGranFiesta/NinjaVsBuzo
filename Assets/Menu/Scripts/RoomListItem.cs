using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour {
  [SerializeField] TMP_Text label;

  RoomInfo info;
  public void SetUp(RoomInfo _info) {
    info = _info;
    label.text = _info.Name;
  }

  public void OnClick() {
        Debug.Log("Joining room: aaa");
        Launcher.Instance.JoinRoom(info);
  }
}
