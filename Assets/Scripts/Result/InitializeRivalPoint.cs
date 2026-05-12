using TMPro;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(TextMeshProUGUI))]
public class InitializeRivalPoint : MonoBehaviour
{
    private void Start()
    {
        // Leemos la puntuación del rival que guardamos en la escena anterior
        int score = PlayerPrefs.GetInt("RivalScore", 0);
        GetComponent<TextMeshProUGUI>().text = string.Format("{0:000}", score);
    }
}
