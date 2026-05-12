using Photon.Pun;
using System.Linq;
using UnityEngine;

public class GameManager
{
    public int ScoreMinePlayer { get; private set; } = 0;

    public void AddPoints(int points)
    {
        ScoreMinePlayer += points;

        MainClass.CustomEvents.OnMineScoreChanged.Invoke();

        if (RivalPointPlayerUI.Instance != null && RivalPointPlayerUI.Instance.photonView != null)
        {
            RivalPointPlayerUI.Instance.photonView.RPC("UpdateScoreRPC", RpcTarget.OthersBuffered, ScoreMinePlayer);
        }
        else
        {
            Debug.LogWarning("GameManager: No se pudo encontrar RivalPointPlayerUI.Instance o su PhotonView.");
        }
    }

    public void MinusScore() {
        ScoreMinePlayer--;
        MainClass.CustomEvents.OnMineScoreChanged.Invoke();

        if (RivalPointPlayerUI.Instance != null && RivalPointPlayerUI.Instance.photonView != null)
        {
            RivalPointPlayerUI.Instance.photonView.RPC("UpdateScoreRPC", RpcTarget.OthersBuffered, ScoreMinePlayer);
        }
        else
        {
            Debug.LogWarning("GameManager: No se pudo encontrar RivalPointPlayerUI.Instance o su PhotonView.");
        }
    }
}
