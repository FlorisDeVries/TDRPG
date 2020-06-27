using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : UnitySingleton<EnemyManager>
{
    // Counters used to keep track of enemy count
    public int EnemiesAlive { get; private set; } = 0;
    public int EnemiesKilled { get; private set; } = 0;
    public int MaxEnemies { get; private set; } = 0;

    [HideInInspector]
    public UnityEvent OnUpdateEnemyCount = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnAllDead = new UnityEvent();

    public void CalculateEnemiesForWave(List<EndlessWaveLogic> wavesLogic)
    {
        int i = 0;
        foreach (EndlessWaveLogic waveLogic in wavesLogic)
        {
            i += waveLogic.CalculateEnemyCount(EndlessWaveManager.Instance.CurrentWave);
        }
        EnemiesKilled = 0;
        EnemiesAlive = 0;
        MaxEnemies = i;
        OnUpdateEnemyCount.Invoke();
    }

    public void RegisterEnemy()
    {
        EnemiesAlive++;
        OnUpdateEnemyCount.Invoke();
    }

    public void EnemyDied()
    {
        EnemiesKilled++;
        EnemiesAlive--;

        // Check whether all enemies died
        if (MaxEnemies > 0)
        {// Max enemies is only >0 in a spawning waves context, so if enemies alive <= 0 would not define all enemies to be dead
            if (IsWaveKilled())
                OnAllDead.Invoke();

        }
        else
        {
            if (EnemiesAlive <= 0)
                OnAllDead.Invoke();
        }

        OnUpdateEnemyCount.Invoke();
    }

    public bool IsWaveKilled()
    {
        Debug.Log($"Max {MaxEnemies} | Killed {EnemiesKilled} | Alive {EnemiesAlive}");
        return EnemiesKilled >= MaxEnemies && EnemiesAlive <= 0;
    }
}
