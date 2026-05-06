using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MineNamePlayerTxt : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName;
    }
}
