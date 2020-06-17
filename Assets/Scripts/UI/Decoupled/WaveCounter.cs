﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Really simple script that just keeps the counter text updated
/// Maybe add some animation later on
/// </summary>
public class WaveCounter : MonoBehaviour
{
    private TMP_Text _text = default;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();

        WaveManager.Instance.OnNextWave.AddListener(UpdateText);
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = WaveManager.Instance.WaveCounter.ToString();
    }
}
