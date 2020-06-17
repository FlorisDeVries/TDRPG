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

    public int WaveCounter { get; private set; } = 1;

    public (int max, int current) EnemiesAlive = (0, 0);

    private List<BaseSpawner> _spawners = new List<BaseSpawner>();

    [HideInInspector]
    public UnityEvent OnNextWave = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnEnemyDied = new UnityEvent();

    private void Start()
    {
        EnemiesAlive = (_currentWave.GetEnemyCount(), _currentWave.GetEnemyCount());
        _currentWave.Reset();
        Reset();

        OnNextWave.Invoke();
    }

    public void EnemyDied()
    {
        EnemiesAlive.current--;
        OnEnemyDied.Invoke();

        if (EnemiesAlive.current <= 0)
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        WaveCounter++;
        _currentWave.NextWave();
        EnemiesAlive = (_currentWave.GetEnemyCount(), _currentWave.GetEnemyCount());

        OnNextWave.Invoke();
    }

    public void AddSpawner(BaseSpawner spawner)
    {
        _spawners.Add(spawner);
        spawner.SetBehaviours(_currentWave.SpawningBehaviours);
    }

    private void Reset()
    {
        WaveCounter = 1;
    }
}
