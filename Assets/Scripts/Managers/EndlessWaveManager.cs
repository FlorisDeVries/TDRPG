using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndlessWaveManager : UnitySingleton<EndlessWaveManager>
{
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

    public float WaveCountdown { get; private set; } = 5f;

    public int CurrentWave { get; private set; } = 0;
    [SerializeField]
    private WaveState _waveState = WaveState.Waiting;

    private List<BaseSpawner> _spawners = new List<BaseSpawner>();

    [HideInInspector]
    public UnityEvent OnUpdateWave = new UnityEvent();

    private void Start()
    {
        _wavesLogic = new List<EndlessWaveLogic>();

        foreach (EndlessWave wave in _waves)
        {
            _wavesLogic.Add(new EndlessWaveLogic(wave));
        }
    }

    private void FixedUpdate()
    {
        if (GameStateManager.Instance.GameState != GameState.Playing)
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
        if (_wavesLogic.Count == 0)
            return;

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
        if (_wavesLogic.Count == 0)
            return;

        // Wait for all enemies to be killed
        if (!EnemyManager.Instance.IsWaveKilled())
            return;

        // Setup for next wave(countdown)
        OnUpdateWave.Invoke();
        WaveCountdown = WaveInterval;
        _waveState = WaveState.Counting;
    }

    private void CountingStateTick()
    {
        if (_wavesLogic.Count == 0)
            return;

        // Countdown and reset spawning when done
        WaveCountdown -= Time.deltaTime;
        if (WaveCountdown > 0)
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

        CurrentWave = 0;
        EnemyManager.Instance.CalculateEnemiesForWave(_wavesLogic);

        OnUpdateWave.Invoke();

        _waveState = WaveState.Counting;
    }

    private void ResetWave()
    {
        foreach (EndlessWaveLogic waveLogic in _wavesLogic)
        {
            waveLogic.Reset();
        }

        EnemyManager.Instance.CalculateEnemiesForWave(_wavesLogic);

        OnUpdateWave.Invoke();
    }
}