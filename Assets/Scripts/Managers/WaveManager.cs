using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : UnitySingleton<WaveManager>
{
    [SerializeField]
    [Tooltip("The current wave that is going to be spawned")]
    private AWave _currentWave = default;
    public AWave CurrentWave { get { return _currentWave; } }

    [SerializeField]
    private int _enemiesAlive = 0;

    private List<BaseSpawner> _spawners = new List<BaseSpawner>();

    private void Start()
    {
        _enemiesAlive = _currentWave.GetEnemyCount();
        _currentWave.Reset();
    }

    public void EnemyDied()
    {
        _enemiesAlive--;

        if (_enemiesAlive <= 0)
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        _currentWave.NextWave();
        _enemiesAlive = _currentWave.GetEnemyCount();
    }

    public void AddSpawner(BaseSpawner spawner)
    {
        _spawners.Add(spawner);
        spawner.SetBehaviours(_currentWave.SpawningBehaviours);
    }
}
