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

    // Start is called before the first frame update
    void Start()
    {
        _attackIcon = GetComponent<Image>();
        if (ProgressionManager.Instance.UnlockedAttacks.Contains(_attack))
        {
            _attackIcon.color = _unlockedColor;
        }
        else
        {
            _attackIcon.color = _lockedColor;
        }
    }
}
