using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AggroController))]
public class BaseEnemy : MonoBehaviour, IDamageable
{
    // Required Components
    protected NavMeshAgent agent = default;
    protected Animator animator = default;
    private AggroController _aggroController = default;

    [Header("Movement control")]
    [SerializeField]
    [Tooltip("How fast this enemy moves")]
    protected float speed = 2f;

    [Header("Life control")]
    [SerializeField]
    [Tooltip("How much HP this enemy starts with")]
    private float _maxHP = 10;
    private float _currentHP = 10;

    [SerializeField]
    [Tooltip("The transform at which the floating text should be displayed")]
    private Transform _floatingTextAnchor = default;

    [SerializeField]
    [Tooltip("The hit particle to play when hit")]
    private GameObject _hitParticles = default;

    [SerializeField]
    [Tooltip("The particle to play when the enemy dies")]
    private GameObject _deathParticles = default;

    [Header("Attack control")]
    [SerializeField]
    [Tooltip("How close the target must be for us to attack")]
    private float _attackRange = 1.5f;
    protected Transform currentTarget = default;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        EnemyManager.Instance.RegisterEnemy();

        // Getting required components
        agent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _aggroController = GetComponent<AggroController>();

        // Setting up HP/movement
        _currentHP = _maxHP;
        agent.speed = speed;

        // GoalManager.Instance
        currentTarget = _aggroController.GetHighestAggro();
        if (currentTarget)
        {
            agent.SetDestination(currentTarget.position);
        }

        GameStateManager.Instance.GameStateEvents[GameState.GameOver].AddListener(OnGameOver);
    }

    protected virtual void Update()
    {
        if (GameStateManager.Instance.GameState != GameState.Playing)
        {
            agent.speed = 0;
            return;
        }
        else
        {
            agent.speed = speed;
        }

        // Get new target each update... Maybe change this to only when targets are added/removed?
        Transform newTarget = _aggroController.GetHighestAggro();
        if (newTarget)
        {
            currentTarget = newTarget;
            agent.SetDestination(currentTarget.position);
        }

        // Attack when in attack range
        if (CanAttack())
            Attack();
    }

    /// <returns>Whether we are in range and ready to attack the target</returns>
    protected virtual bool CanAttack()
    {
        return currentTarget && Vector3.Distance(transform.position, currentTarget.position) < _attackRange;
    }


    /// <summary>
    /// Called when this entity gets hit
    /// </summary>
    /// <param name="damage">How many damage is inflicted</param>
    /// <param name="position">Where were we hit</param>
    /// <param name="direction">In what direction where we hit</param>
    public void GetHit(float damage, Vector3 position, Vector3 direction)
    {
        if (_currentHP <= 0)
            return;

        // Take damage
        _currentHP -= damage;

        // Play particle hitEffect
        if (_hitParticles)
        {
            GameObject hitEffect = Instantiate(_hitParticles, position + direction.normalized * 0.5f, Quaternion.FromToRotation(Vector3.up, direction));
            hitEffect.transform.forward = direction;
        }


        // Animation
        animator?.SetTrigger("GetHit");

        // Check whether we died
        if (_currentHP <= 0)
        {
            Die();
        }
        else
        {
            // Spawn damage numbers
            FloatingText floatingText = Instantiate(PrefabManager.Instance.FloatingText, _floatingTextAnchor.position, Quaternion.Euler(-Camera.main.transform.forward)).GetComponent<FloatingText>();
            floatingText.SetText(_currentHP.ToString());
            floatingText.SetColor(Color.red);
        }
    }

    /// /// <summary>
    /// Called when this entity gets hit
    /// </summary>
    /// <param name="damage">How many damage is inflicted</param>
    /// <param name="position">Where were we hit</param>
    /// <param name="direction">In what direction where we hit</param>
    /// <param name="transmitter">What aggro transmitter hit us</param>
    public void GetHit(float damage, Vector3 position, Vector3 direction, AggroTransmitter transmitter)
    {
        // Do something with the transmitter
        _aggroController.AddAggro(transmitter, damage);

        // Handle hit
        GetHit(damage, position, direction);
    }

    /// <summary>
    /// Attacks the current target
    /// </summary>
    protected virtual void Attack()
    {

    }

    /// <summary>
    /// Called when the player loses the game
    /// </summary>
    protected virtual void OnGameOver()
    {
        agent.enabled = false;
    }

    /// <summary>
    /// Called when this enemy dies
    /// </summary>
    protected virtual void Die()
    {
        // Mark as dead in waveManager
        EnemyManager.Instance.EnemyDied();

        animator?.SetBool("Alive", false);
        if (_deathParticles)
            Instantiate(_deathParticles, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        Destroy(this.gameObject);
    }

    #region Cheats
    public void OnKillAll()
    {
        Die();
    }
    #endregion
}
