using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetratingProjectile : BaseProjectile
{
    [Header("Penetrating")]
    [SerializeField]
    [Tooltip("How many targets can we penetrate before dying")]
    private int _penetrationDepth = 1;
    private int _currentDepth = 0;

    [SerializeField]
    [Tooltip("What layers can we penetrate")]
    private LayerMask _penetrationMask = default;


    protected override void OnTriggerEnter(Collider other)
    {
        HitOther(other);

        // If we have reached our penetrating capabilities we die
        if (_currentDepth >= _penetrationDepth || !Utils.IsInLayerMask(other.gameObject.layer, _penetrationMask))
            Die();
        _currentDepth++;
    }
}
