using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Really simple script that just keeps the remaining enemies bar up to date
/// </summary>
public class RemainingEnemies : MonoBehaviour
{
    private TMP_Text _text = default;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();

        EnemyManager.Instance.OnUpdateEnemyCount.AddListener(UpdateText);
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = $"{EnemyManager.Instance.EnemiesAlive} / {EnemyManager.Instance.MaxEnemies}";
    }
}
