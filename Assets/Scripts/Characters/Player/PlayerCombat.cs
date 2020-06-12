using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Class that handles all combat related logic for the player
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    [Tooltip("The attacks currently available in the hotbar")]
    [SerializeField]
    private List<AnAttack> _currentAttacks = new List<AnAttack>();
    private AnAttack _currentAttack = default;

    [SerializeField]
    [Tooltip("Prefab used to create the hotbar")]
    private GameObject _hotBarPrefab = default;

    [SerializeField]
    [Tooltip("Parent tranform for the hotbar, where all hotbar items should be child")]
    private Transform _hotbarParent = default;

    [Header("Projectiles")]
    private bool _firing = false;

    [SerializeField]
    [Tooltip("The position for the projectiles to be fired")]
    private Transform _firePoint = default;

    [SerializeField]
    private int _attackIndex = 0;

    private void Start()
    {
        foreach (AnAttack attack in _currentAttacks)
            attack.SetupAttack(_hotbarParent, _hotBarPrefab);

        if (_currentAttacks.Count > 0)
        {
            _currentAttacks[_attackIndex].SetSelected(true);
            _currentAttack = _currentAttacks[_attackIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Firing
        if (_firing && _firePoint)
        {
            _currentAttack?.TrySpawn(_firePoint);
        }

        foreach (ASpawnable attack in _currentAttacks)
        {
            attack.Tick();
        }
    }

    private void SetNewAttack(int index)
    {
        _attackIndex = index;
        _currentAttack?.SetSelected(false);

        if (_currentAttacks.Count > index)
        {
            _currentAttack = _currentAttacks[index];
            _currentAttack.SetSelected(true);
        }
    }

    public void AddAttack(AnAttack attack)
    {
        _currentAttacks.Add(attack);
        attack.SetupAttack(_hotbarParent, _hotBarPrefab);

        if (_currentAttacks.Count == 1)
            SetNewAttack(0);
    }

    #region Input
    public void OnFire()
    {
        _firing = !_firing;
    }

    public void OnChangeWeapon1()
    {
        SetNewAttack(0);
    }

    public void OnChangeWeapon2()
    {
        SetNewAttack(1);
    }

    public void OnChangeWeapon(InputValue value)
    {
        int change = (int)value.Get<float>();
        if (change == 0)
            return;
        change /= Mathf.Abs(change);

        int newIdx = _attackIndex + change;

        if (newIdx >= _currentAttacks.Count)
            newIdx = 0;

        if (newIdx < 0)
            newIdx = _currentAttacks.Count - 1;

        SetNewAttack(newIdx);
    }
    #endregion
}
