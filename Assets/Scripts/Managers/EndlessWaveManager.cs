using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndlessWaveManager : UnitySingleton<EndlessWaveManager>
{
    [System.Serializable]
    enum WaveState
    {
        Spawning,
        Waiting,
        Counting
    }

    [SerializeField]
    [Tooltip("The endless waves that this endlessWaveManager will be spawning")]
    private List<EndlessWave> _waves = new List<EndlessWave>();
    // The logic aspect of the waves, so the waves do not get edited
    private List<EndlessWaveLogic> _wavesLogic = new List<EndlessWaveLogic>();

    [SerializeField]
    [Tooltip("How much time there should be in between waves")]
    private float WaveInterval = 5f;
    [SerializeField]// For debug
    private float _waveCountdown = 5f;


    public int CurrentWave { get; private set; } = 1;
    [SerializeField]
    private WaveState _waveState = WaveState.Waiting;

    [SerializeField]
    public (int max, int current) EnemiesAlive = (0, 0);

    private List<BaseSpawner> _spawners = new List<BaseSpawner>();

    [HideInInspector]
    public UnityEvent OnUpdateWave = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnUpdateEnemyCount = new UnityEvent();

    private void Start()
    {
        Reset();
    }

    private void FixedUpdate()
    {
        if (GameStateManager.Instance.GameState == GameState.GameOver)
            return;

        switch (_waveState)
        {
            case WaveState.Spawning:
                SpawningStateTick();
                break;
            case WaveState.Waiting:
                WaitingStateTick();
                break;
            case WaveState.Counting:
                CountingStateTick();
                break;
        }

    }

    #region WaveStates
    private void SpawningStateTick()
    {
        bool done = true;
        foreach (EndlessWaveLogic behaviour in _wavesLogic)
        {
            if (behaviour.ShouldSpawn())
                behaviour.Spawn(GetRandomSpawner(), CurrentWave);
            done = done && behaviour.AllSpawned(CurrentWave);
        }
        if (done)
            _waveState = WaveState.Waiting;
    }

    private void WaitingStateTick()
    {
        // Wait for all enemies to be killed
        if (EnemiesAlive.current > 0)
            return;

        // Setup for next wave(countdown)
        OnUpdateWave.Invoke();
        _waveCountdown = WaveInterval;
        _waveState = WaveState.Counting;
    }

    private void CountingStateTick()
    {
        // Countdown and reset spawning when done
        _waveCountdown -= Time.deltaTime;
        if (_waveCountdown > 0)
            return;

        CurrentWave++;
        ResetWave();
        _waveState = WaveState.Spawning;
    }
    #endregion

    private BaseSpawner GetRandomSpawner()
    {
        return _spawners[Random.Range(0, _spawners.Count)];
    }

    public void EnemyDied()
    {
        EnemiesAlive.current--;
        OnUpdateEnemyCount.Invoke();
    }

    public void AddSpawner(BaseSpawner spawner)
    {
        _spawners.Add(spawner);
    }

    private void Reset()
    {
        _wavesLogic = new List<EndlessWaveLogic>();

        foreach (EndlessWave wave in _waves)
        {
            _wavesLogic.Add(new EndlessWaveLogic(wave));
        }

        CurrentWave = 1;
        CalculateEnemies();

        OnUpdateWave.Invoke();
        OnUpdateEnemyCount.Invoke();

        _waveState = WaveState.Counting;
    }

    private void ResetWave()
    {
        foreach (EndlessWaveLogic waveLogic in _wavesLogic)
        {
            waveLogic.Reset();
        }

        CalculateEnemies();

        OnUpdateWave.Invoke();
        OnUpdateEnemyCount.Invoke();
    }

    private void CalculateEnemies()
    {
        int i = 0;
        foreach (EndlessWaveLogic waveLogic in _wavesLogic)
        {
            i += waveLogic.CalculateEnemyCount(CurrentWave);
        }
        EnemiesAlive = (i, i);
    }
}