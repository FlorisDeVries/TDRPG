using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How fast the projectile is going")]
    private float _speed = 20f;

    [SerializeField]
    [Tooltip("How many of these projectiles can be shot per second")]
    private float _fireRate = 4f;

    [SerializeField]
    [Tooltip("How long the projectile can life on it's own")]
    private float _lifeTime = 10f;

    public float FireRate { get { return _fireRate; } }

    [SerializeField]
    [Tooltip("Prefab for the muzzle flash and hit flash")]
    private GameObject _muzzlePrefab = default, _hitPrefab = default;

    private void Start()
    {
        if (_muzzlePrefab)
        {
            GameObject muzzle = Instantiate(_muzzlePrefab, transform.position, transform.rotation);
            // muzzle.transform.forward = transform.forward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0f)
            Die(transform.position, Quaternion.FromToRotation(Vector3.up, -transform.forward));
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        _speed = 0;
        ContactPoint contact = other.contacts[0];

        BasicEnemy enemy = other.gameObject.GetComponent<BasicEnemy>();
        if (enemy)
        {
            enemy.Hit(contact.point, transform.forward);
        }

        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Die(contact.point, rot);
    }

    private void Die(Vector3 pos, Quaternion rot)
    {
        Destroy(gameObject);
        if (_hitPrefab)
            Instantiate(_hitPrefab, transform.position, transform.rotation);
    }
}
