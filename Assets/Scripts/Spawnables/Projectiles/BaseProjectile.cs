﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [Header("Base Projectile")]
    [SerializeField]
    [Tooltip("How fast the projectile is going")]
    private float _speed = 20f;

    [SerializeField]
    [Tooltip("How long the projectile can life on it's own")]
    private float _lifeTime = 10f;

    [SerializeField]
    [Tooltip("How much damage this projectile deals on hit")]
    private float _damage = 5;

    [SerializeField]
    [Tooltip("Prefab for the muzzle flash and hit flash")]
    private GameObject _spawnParticles = default, _hitParticles = default;

    [SerializeField]
    [Tooltip("What layers this projectile can hit")]
    private LayerMask _hitMask = default;

    [SerializeField]
    [Tooltip("What aggrotransmitter send this attack")]
    private AggroTransmitter _transmitter = default;

    protected virtual void Start()
    {
        if (_spawnParticles)
            Instantiate(_spawnParticles, transform.position, transform.rotation);
    }

    public void SetupProjectile(AggroTransmitter transmitter)
    {
        _transmitter = transmitter;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0f)
            Die();
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        _speed = 0;

        HitOther(other);

        Die();
    }

    protected virtual void HitOther(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null && Utils.IsInLayerMask(other.gameObject.layer, _hitMask))
            if (_transmitter == null)
                damageable.GetHit(_damage, transform.position, transform.forward);
            else
                damageable.GetHit(_damage, transform.position, transform.forward, _transmitter);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        if (_hitParticles)
            Instantiate(_hitParticles, transform.position, transform.rotation);
    }
}
