using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour, IDamageable
{
    private float _maxHP = 1;
    private float _currentHP = 1;

    public virtual void Setup(Tower tower)
    {
        _maxHP = tower.HealtPoints;
        _currentHP = _maxHP;
    }

    public virtual void Hit(float damage)
    {
        _currentHP -= damage;
        if (_currentHP <= 0)
            Destroy(this.gameObject);
    }
}
