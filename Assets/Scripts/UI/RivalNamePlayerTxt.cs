using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RivalNamePlayerTxt : MonoBehaviour
{
    private void Start()
    {
        if (PhotonNetwork.PlayerListOthers.Length > 0)
        {
            // Tomamos el nombre del primer jugador que no seamos nosotros (el rival)
            GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerListOthers[0].NickName;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "No Player";
        }
    }
}
