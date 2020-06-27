using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Really simple script that just keeps the remaining enemies bar up to date
/// </summary>
public class RemainingEnemies : MonoBehaviour
{
    private TMP_Text _text = default;

    [SerializeField]
    [Tooltip("The rect transform used as progress bar")]
    private RectTransform _fillRect = default;

    [SerializeField]
    private float _originalWidth = 0;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _originalWidth = Mathf.Abs(_fillRect.offsetMax.x);

        EnemyManager.Instance.OnUpdateEnemyCount.AddListener(UpdateText);
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = $"{EnemyManager.Instance.EnemiesAlive} / {EnemyManager.Instance.MaxEnemies}";
        if (EnemyManager.Instance.MaxEnemies > 0)
            SetCooldownProgress(EnemyManager.Instance.EnemiesAlive / (float)EnemyManager.Instance.MaxEnemies);
    }

    /// <summary>
    /// Sets how far the cooldown is done loading
    /// </summary>
    /// <param name="scale">1 = fully done, 0 is completely reset</param>
    public void SetCooldownProgress(float scale)
    {
        float topOffset = -scale * _originalWidth;
        _fillRect.offsetMax = new Vector2(topOffset, 0);
    }
}
