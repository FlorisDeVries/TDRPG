using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour, IDamageable
{
    private float _maxHP = 1;
    private float _currentHP = 1;

    public virtual void Setup(ATower tower)
    {
        _maxHP = tower.HealtPoints;
        _currentHP = _maxHP;
    }

    public virtual void GetHit(float damage, Vector3 position, Vector3 direction)
    {
        _currentHP -= damage;
        if (_currentHP <= 0)
            Die();
    }

    public void GetHit(float damage, Vector3 position, Vector3 direction, AggroTransmitter transmitter)
    {
        GetHit(damage, position, direction);
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
