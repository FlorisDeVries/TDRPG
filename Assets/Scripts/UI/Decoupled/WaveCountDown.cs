using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCountDown : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text component that displays the countdown timer")]
    private TMP_Text _countdownText = default;

    // Update is called once per frame
    void Update()
    {
        int countdown = (int)EndlessWaveManager.Instance.WaveCountdown;
        if (countdown > 0)
        {
            _countdownText.text = countdown.ToString();
            _countdownText.enabled = true;
        }
        else
            _countdownText.enabled = false;
    }
}
