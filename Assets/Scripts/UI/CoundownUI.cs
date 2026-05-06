using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoundownUI : MonoBehaviour
{
    private int countdownTime = 360;
    private TextMeshProUGUI countdownText;

    void Awake()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
        countdownText.text = countdownTime.ToString();
        StartCoroutine(UpdateCountdown());
    }

    private IEnumerator UpdateCountdown()
    {
        while (countdownTime > 0)
        {
            countdownText.text = String.Format("{0:000}", countdownTime);
            yield return new WaitForSeconds(1);
            countdownTime--;
        }
        //TODO LOAD SCENE RESULT
    }
}
