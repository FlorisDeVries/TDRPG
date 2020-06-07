using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy class for a really basic enemy that just walks towards the target and starts attacking
/// </summary>
public class MeleeEnemy : BaseEnemy
{
    [SerializeField]
    [Tooltip("How much damage we do each hit")]
    protected float damage = 5f;
    private bool _attacking = false;

    protected override void Attack()
    {
        _attacking = true;
        agent.speed = 0;
        // Start playing attack animation
        animator.SetBool("Attacking", true);
    }

    protected override bool CanAttack()
    {
        return base.CanAttack() && !_attacking;
    }

    protected virtual void StopAttacking()
    {
        _attacking = false;
        agent.speed = speed;
        // Stop attacking animation
        animator.SetBool("Attacking", false);
    }

    /// <summary>
    /// At the end of the attack animation this checks whether we should continue attacking, if not we stop
    /// </summary>
    protected virtual void CheckContinueAttacking()
    {
        if (!base.CanAttack())
            StopAttacking();

    }

    /// <summary>
    /// Strikes the target, dealing damage
    /// </summary>
    protected virtual void DoMeleeDamage()
    {
        if (base.CanAttack())
        {
            IDamageable damageable = currentTarget.GetComponentInParent<IDamageable>();
            damageable.GetHit(damage, currentTarget.position, (currentTarget.position - transform.position).normalized);
        }
    }
}
