using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;

    // AÑADIDO: Especificar Photon.Realtime.Player
    Photon.Realtime.Player player;

    public void SetUp(Photon.Realtime.Player _player)
    {
        Debug.Log("PlayerListItem - SetUp");
        player = _player;
        text.text = _player.NickName;
    }

    // AÑADIDO: Especificar Photon.Realtime.Player
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("PlayerListItem - OnPlayerLeftRoom");
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("PlayerListItem - OnLeftRoom");
        Destroy(gameObject);
    }
}