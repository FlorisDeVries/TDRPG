using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new EndlessWave", menuName = "Waves/EndlessWave")]
/// <summary>
/// Defines the endless wave spawning behaviour for one enemy stype
/// </summary>
public class EndlessWave : ScriptableObject
{
    [Header("Spawning Properties")]
    [Tooltip("The basic prefab to be spawned by this spawning behaviour")]
    public GameObject Prefab = default;

    // Used to calculate the number of enemies per wave
    public int Base = 1;
    public float Multiplier = 2f;
    public int WaveDelay = 1;

    [Tooltip("How much time should be between each spawn")]
    public float SpawnInterval = .1f;
}

/// <summary>
/// Wrapper class to make more sense of the endless waves system
/// </summary>
public class EndlessWaveLogic
{
    private EndlessWave _wave = default;
    private int _spawnedAmount = 0;
    private float _spawnTimer = 0;
    private bool _allDone = false;

    public EndlessWaveLogic(EndlessWave endlessWave)
    {
        _wave = endlessWave;
        Reset();
    }

    public bool ShouldSpawn()
    {
        if (_allDone)
            return false;

        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < _wave.SpawnInterval)
        {
            return true;
        }
        return false;
    }

    public void Spawn(BaseSpawner spawner, int currentWave)
    {
        spawner.Spawn(_wave.Prefab);
        _spawnedAmount++;
        _allDone = AllSpawned(currentWave);
    }

    public void Reset()
    {
        _spawnedAmount = 0;
        _spawnTimer = _wave.SpawnInterval;
        _allDone = false;
    }

    public int CalculateEnemyCount(int currentWave)
    {
        return _wave.Base + (int)Mathf.Max(_wave.Multiplier * (currentWave - _wave.WaveDelay));
    }

    public bool AllSpawned(int currentWave)
    {
        return _spawnedAmount >= CalculateEnemyCount(currentWave);
    }
}
