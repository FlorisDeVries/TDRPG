using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    [Tooltip("Max HP of the player")]
    private float _maxHP = 50f;
    private float _currentHP = 50f;
    public bool IsDead { get; private set; } = false;

    [SerializeField]
    [Tooltip("How fast the player regenerates health points")]
    private float _regenRate = 1;
    [SerializeField]
    [Tooltip("How many health point the player regains each regenaration tick")]
    private float _regenAmount = 1;
    private float _regenTimer = 0;

    [SerializeField]
    [Tooltip("The transform at which the floating text should be displayed")]
    private Transform _floatingTextAnchor = default;

    private void Start()
    {
        _currentHP = _maxHP;

        GameStateManager.Instance.OnGameOver.AddListener(Die);
        EndlessWaveManager.Instance.OnUpdateWave.AddListener(Respawn);
    }

    private void Update()
    {
        if (_currentHP < _maxHP)
        {
            _regenTimer -= Time.deltaTime;
            if (_regenTimer <= 0)
                HealthRegen();
        }
    }

    public void HealthRegen()
    {
        _currentHP += _regenAmount;
        _currentHP = Mathf.Min(_maxHP, _currentHP);
        _regenTimer = _regenRate;
    }

    public virtual void Die()
    {
        if (IsDead)
            return;
        IsDead = true;
    }

    public virtual void Respawn()
    {
        if (!IsDead)
            return;
        IsDead = false;

        _regenTimer = _regenRate;
    }

    public void GetHit(float damage, Vector3 position, Vector3 direction)
    {
        if (_currentHP >= _maxHP)
            _regenTimer = _regenRate;

        _currentHP -= damage;
        if (_currentHP <= 0)
            Die();
        else
        {
            // Spawn damage numbers
            FloatingText floatingText = Instantiate(PrefabManager.Instance.FloatingText, _floatingTextAnchor.position, Quaternion.Euler(-Camera.main.transform.forward)).GetComponent<FloatingText>();
            floatingText.SetText(_currentHP.ToString());
            floatingText.SetColor(Color.red);
        }
    }
}
