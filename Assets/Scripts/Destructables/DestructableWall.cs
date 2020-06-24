using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour, IDamageable
{
    [SerializeField]
    [Tooltip("How much hitPoints this destructable takes before it is destroyed")]
    private float _hitPoints = 10;
    public void GetHit(float damage, Vector3 position, Vector3 direction)
    {
        _hitPoints -= damage;
        if (_hitPoints < 0)
            Destroy(this.gameObject);
    }
}
