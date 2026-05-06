using Photon.Pun;
using System.Linq;
using UnityEngine;

public class GameManager
{
    public int ScoreMinePlayer { get; private set; }

    public void AddPoints(int points)
    {
        ScoreMinePlayer += points;
        
        // Notificamos a nuestra UI local
        MainClass.CustomEvents.OnMineScoreChanged.Invoke();
        
        // Notificamos a los demás a través del RPC en la UI del rival
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
