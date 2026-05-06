using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MinePointPlayerUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // Suscribirse al evento global de cambio de puntuación
        MainClass.CustomEvents.OnMineScoreChanged.AddListener(UpdateUI);
        
        // Actualizar inicialmente
        UpdateUI();
    }

    private void OnDestroy()
    {
        // Limpieza para evitar fugas de memoria
        if (MainClass.CustomEvents != null)
        {
            MainClass.CustomEvents.OnMineScoreChanged.RemoveListener(UpdateUI);
        }
    }

    public void UpdateUI()
    {
        text.text = String.Format("{0:000}", MainClass.GameManager.ScoreMinePlayer);
    }
}
