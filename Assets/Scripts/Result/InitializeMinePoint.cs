using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class InitializeMinePoint : MonoBehaviour
{
    private void Start()
    {
        // Obtenemos la puntuación guardada en el GameManager local
        if (MainClass.GameManager != null)
        {
            GetComponent<TextMeshProUGUI>().text = string.Format("{0:000}", MainClass.GameManager.ScoreMinePlayer);
        }
    }
}
