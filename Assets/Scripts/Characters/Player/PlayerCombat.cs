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
    private PlayerHealth _playerHealth;

    [Tooltip("The attacks currently available in the hotbar")]
    [SerializeField]
    private List<AnAttack> _startAttacks = new List<AnAttack>();
    private List<AnAttackLogic> _currentAttacks = new List<AnAttackLogic>();
    private AnAttackLogic _currentAttack = default;

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
    private AggroTransmitter _aggroTransmitter = default;

    private int _attackIndex = 0;

    private void Start()
    {
        // GetComponents
        _aggroTransmitter = GetComponentInChildren<AggroTransmitter>();

        // Add all unlocked attacks to the player kit
        List<AnAttack> unlockedAttacks = ProgressionManager.Instance.UnlockedAttacks;
        foreach (AnAttack a in unlockedAttacks)
        {
            if (!_startAttacks.Contains(a))
                _startAttacks.Add(a);
        }

        _playerHealth = GetComponent<PlayerHealth>();

        foreach (AnAttack attack in _startAttacks)
        {
            AnAttackLogic attackLogic;
            if (attack.GetType() == typeof(AnAttackTower))
                attackLogic = new AnAttackTowerLogic(attack);
            else if (attack.GetType() == typeof(ATower))
                attackLogic = new ATowerLogic(attack);
            else
                attackLogic = new AnAttackLogic(attack, _aggroTransmitter);

            attackLogic.SetupAttack(_hotbarParent, _hotBarPrefab);
            _currentAttacks.Add(attackLogic);
        }

        if (_currentAttacks.Count > 0)
        {
            _currentAttacks[_attackIndex].SetSelected(true);
            _currentAttack = _currentAttacks[_attackIndex];
        }
        else
        {
            _hotbarParent.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ASpawnableLogic attack in _currentAttacks)
        {
            attack.Tick();
        }

        if (_playerHealth.IsDead || GameStateManager.Instance.GameState != GameState.Playing)
            return;

        // Firing
        if (_firing && _firePoint)
        {
            _currentAttack?.TrySpawn(_firePoint.position, _firePoint.rotation);
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
        if (_startAttacks.Contains(attack))
            return;

        if (_currentAttacks.Count == 0)
            _hotbarParent.gameObject.SetActive(true);

        AnAttackLogic newAttack;
        if (attack.GetType() == typeof(AnAttackTower))
            newAttack = new AnAttackTowerLogic(attack);
        else if (attack.GetType() == typeof(ATower))
            newAttack = new ATowerLogic(attack);
        else
            newAttack = new AnAttackLogic(attack, _aggroTransmitter);

        _currentAttacks.Add(newAttack);
        newAttack.SetupAttack(_hotbarParent, _hotBarPrefab);

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
