using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class NamePlayerUI : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName;
    }
}
