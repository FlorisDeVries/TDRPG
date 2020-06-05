using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class BasicEnemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    [Tooltip("Nav mesh agent")]
    private NavMeshAgent _agent = default;

    [SerializeField]
    [Tooltip("How close the target must be for us to attack")]
    private float _attackRange = 1.5f;

    // The current target goal
    private Transform _target = default;

    [SerializeField]
    [Tooltip("The hit particle to play when hit")]
    private GameObject _hitParticles = default;

    [SerializeField]
    [Tooltip("The particle to play when the enemy dies")]
    private GameObject _deathParticles = default;

    private float _healthPoints = 3;

    private Animator _animator;

    private AggroController _aggroController = default;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _aggroController = GetComponent<AggroController>();

        // GoalManager.Instance
        _target = _aggroController.GetHighestAggro();
        if (_target)
            _agent.SetDestination(_target.position);
    }

    private void Update()
    {
        Transform newTarget = _aggroController.GetHighestAggro();
        if (newTarget)
        {
            _target = newTarget;
            _agent.SetDestination(_target.position);
        }

        if (Vector3.Distance(transform.position, _target.position) < _attackRange)
            Explode();
    }

    public void Hit(float damage, Vector3 pos, Vector3 direction)
    {
        GameObject hitEffect = Instantiate(_hitParticles, pos + direction.normalized * 0.5f, Quaternion.FromToRotation(Vector3.up, direction));
        hitEffect.transform.forward = direction;

        _healthPoints--;
        if (_healthPoints <= 0)
            Explode();
    }

    public void Explode()
    {
        _agent.SetDestination(transform.position);
        _animator.SetBool("Alive", false);
        Instantiate(_deathParticles, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        Destroy(this.gameObject);
    }
}
