using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackTower : BaseTower
{
    [Header("Shooting Properties")]
    [SerializeField]
    [Tooltip("What to rotate to aim the turret")]
    private Transform _rotationPoint = default;

    [SerializeField]
    [Tooltip("The point at which the attack should spawn")]
    private Transform _firePoint = default;

    // Attack properties
    private GameObject _attack = default;
    private float _targetRange = default;
    private LayerMask _targetMask = default;
    private float _rotationSpeed = 1f;
    private float _cooldown = .5f;
    private float _cooldownTimer = 0;
    // What targets are in range
    private List<Transform> _targetTransforms = default;

    public override void Setup(ATower tower)
    {
        base.Setup(tower);
        if (tower.GetType() != typeof(AnAttackTower))
        {
            Debug.LogError($"Wrong tower type defined in {gameObject.name} | {this.name}");
            Destroy(this.gameObject);
        }

        // Setup attack tower properties (copy them)
        AnAttackTower attackTower = tower as AnAttackTower;

        _attack = attackTower.TowerAttack.Attack;
        _cooldown = attackTower.TowerAttack.Cooldown;

        _targetRange = attackTower.AttackRange;
        _targetMask = attackTower.TargetMask;
        _rotationSpeed = attackTower.RotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Update timers
        _cooldownTimer += Time.deltaTime;

        // Get objects in range
        _targetTransforms = new List<Transform>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _targetRange, _targetMask);

        // Only select the Damageable objects
        foreach (Collider col in hitColliders)
        {
            if (col.GetComponent<IDamageable>() != null)
            {
                _targetTransforms.Add(col.transform);
            }
        }

        // Sort on distance
        _targetTransforms.Sort(
            (x1, x2) =>
            Vector3.Distance(x1.position, transform.position).CompareTo(
                Vector3.Distance(x2.position, transform.position)));

        // If we have a target
        if (_targetTransforms.Count > 0)
        {
            TakeAim();

            if (_cooldownTimer > _cooldown)
            {
                Fire();
            }
        }
    }

    protected virtual void TakeAim()
    {
        Quaternion OriginalRot = _rotationPoint.rotation;
        _rotationPoint.LookAt(_targetTransforms[0]);
        Quaternion NewRot = _rotationPoint.rotation;
        _rotationPoint.rotation = OriginalRot;
        _rotationPoint.rotation = Quaternion.RotateTowards(_rotationPoint.rotation, NewRot, _rotationSpeed * 100 * Time.deltaTime);
    }

    protected virtual void Fire()
    {
        Instantiate(_attack, _firePoint.position, _firePoint.rotation);
        _cooldownTimer = 0;
    }

    void OnDrawGizmosSelected()
    {
        // Debug for range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _targetRange);
    }
}