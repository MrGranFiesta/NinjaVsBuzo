using Photon.Pun;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RivalPointPlayerUI : MonoBehaviourPunCallbacks
{
    public static RivalPointPlayerUI Instance;
    private TextMeshProUGUI text;

    private void Awake()
    {
        Instance = this;
        text = GetComponent<TextMeshProUGUI>();
    }

    [PunRPC]
    public void UpdateScoreRPC(int score)
    {
        if (text != null)
        {
            text.text = String.Format("{0:000}", score);
        }
    }
}
