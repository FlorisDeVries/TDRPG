using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarItem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The image that the icon should be in")]
    private Image _icon = default;

    [SerializeField]
    [Tooltip("Image used to convey selected hotbar item")]
    private Image _selected = default;

    [SerializeField]
    [Tooltip("The cooldown overlay")]
    private RectTransform _cooldownRect = default;

    /// <summary>
    /// Sets how far the cooldown is done loading
    /// </summary>
    /// <param name="scale">1 = fully done, 0 is completely reset</param>
    public void SetCooldownProgress(float scale)
    {
        float topOffset = -scale * 135;
        _cooldownRect.offsetMax = new Vector2(0, topOffset);
    }

    public void SetImage(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    public void SetSelected(bool value)
    {
        _selected.enabled = value;
    }
}
