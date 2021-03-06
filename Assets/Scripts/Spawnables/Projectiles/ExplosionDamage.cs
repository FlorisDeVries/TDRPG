﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    [SerializeField]
    [Tooltip("What layers the explosion should hit")]
    private LayerMask _explosionMask = default;

    [SerializeField]
    [Tooltip("How big the explosion will be")]
    private float _explosionRange = 2f;

    private void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRange, _explosionMask);

        foreach (Collider col in hitColliders)
        {
            IDamageable damageable = col.GetComponentInChildren<IDamageable>();
            if (damageable != null)
                damageable.GetHit(5f, col.transform.position, (col.transform.position - transform.position).normalized);
        }
    }
}
