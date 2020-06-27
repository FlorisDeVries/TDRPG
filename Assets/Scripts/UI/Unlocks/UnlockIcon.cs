using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UnlockIcon : MonoBehaviour
{
    private Image _attackIcon = default;

    [SerializeField]
    [Tooltip("The unlockable attack")]
    private AnAttack _attack = default;

    [SerializeField]
    [Tooltip("The unlocked color")]
    private Color _unlockedColor = default;

    [SerializeField]
    [Tooltip("The locked color")]
    private Color _lockedColor = default;

    [SerializeField]
    [Tooltip("The locked color")]
    private Color _hiddenColor = default;

    [SerializeField]
    [Tooltip("Whether this pickup is hidden")]
    private bool _hidden = false;

    [SerializeField]
    [Tooltip("The hidden overlay")]
    private GameObject _hiddenOverlay = default;

    // Start is called before the first frame update
    void Start()
    {
        _attackIcon = GetComponent<Image>();
        _attackIcon.sprite = _attack.Sprite;
        if (ProgressionManager.Instance.UnlockedAttacks.Contains(_attack))
        {
            if (_hidden)
            {
                _hiddenOverlay?.SetActive(false);
            }
            _attackIcon.color = _unlockedColor;
        }
        else
        {
            _attackIcon.color = _lockedColor;
            if (_hidden)
            {
                _attackIcon.color = _hiddenColor;
                _hiddenOverlay?.SetActive(true);
            }
        }
    }
}
