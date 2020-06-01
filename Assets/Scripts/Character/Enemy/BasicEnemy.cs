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

    // The current target goal
    private Goal _goal = default;

    [SerializeField]
    [Tooltip("The hit particle to play when hit")]
    private GameObject _hitParticles = default;

    [SerializeField]
    [Tooltip("The particle to play when the enemy dies")]
    private GameObject _deathParticles = default;

    private float _healthPoints = 3;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        // GoalManager.Instance
        _goal = GoalManager.Instance.GetClosestGoal(transform.position);
        if (_goal)
            _agent.SetDestination(_goal.transform.position);
    }

    public void Hit(float damage, Vector3 pos, Vector3 direction)
    {
        GameObject hitEffect = Instantiate(_hitParticles, pos + direction.normalized * 0.5f, Quaternion.FromToRotation(Vector3.up, direction));
        hitEffect.transform.forward = direction;

        _healthPoints--;
        if (_healthPoints <= 0)
            Explode();
    }

    // TODO: Do this or check for distance each update?
    private void OnCollisionEnter(Collision other)
    {
        Goal goal = other.gameObject.GetComponent<Goal>();
        if (goal)
        {
            Explode();
        }
    }

    public void Explode()
    {
        _agent.SetDestination(transform.position);
        _animator.SetBool("Alive", false);
        Instantiate(_deathParticles, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        Destroy(this.gameObject);
    }
}
