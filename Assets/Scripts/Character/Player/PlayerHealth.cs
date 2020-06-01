using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    [Tooltip("Max HP of the player")]
    private float _maxHP = 50f;
    private float _currentHP = 50f;

    private void Start()
    {
        _currentHP = _maxHP;
    }

    public void Die()
    {
        Destroy(this.gameObject);

        Debug.Log($"Player died");
    }

    public void Hit(float damage, Vector3 position, Vector3 direction)
    {
        _currentHP -= damage;
        if(_currentHP <= 0)
            Die();
    }
}
