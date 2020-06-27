using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour, IDamageable
{
    [SerializeField]
    [Tooltip("How much HP this goal has")]
    private float _hitPoints = 2;

    public void GetHit(float damage, Vector3 position, Vector3 direction)
    {
        _hitPoints -= damage;
        if (_hitPoints <= 0)
            Die();
    }

    public void GetHit(float damage, Vector3 position, Vector3 direction, AggroTransmitter transmitter)
    {
        GetHit(damage, position, direction);
    }

    private void Die()
    {
        Destroy(gameObject);

        GameStateManager.Instance.SetGameState(GameState.GameOver);
    }
}
