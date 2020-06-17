using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object used to describe a spawning behaviour used in spawners
/// </summary>
[CreateAssetMenu(fileName = "new SpawningBehaviour", menuName = "SpawningBehaviour/Basic")]
public class BaseSpawningBehaviour : ScriptableObject
{
    [Header("Spawning Properties")]
    [Tooltip("The basic spawnable to be spawned by this spawning behaviour")]
    public ASpawnable Spawnable = default;

    [Tooltip("How many of the spawnable should be spawned before this behaviour is finished")]
    public int SpawnAmount = 5;
    private int _spawnCounter = 0;

    [Tooltip("How long the spawner should wait in between spawning")]
    public float SpawnerInterval = 1f;
    private float _intervalTimer = 0f;

    [Tooltip("How long the spawner should wait after spawning this wave")]
    public float SpawnerCooldown = 1f;
    private float _cooldownTimer = 0f;

    public void Reset()
    {
        _intervalTimer = 0;
        _cooldownTimer = SpawnerCooldown;
        _spawnCounter = 0;
    }

    public bool IsDone()
    {
        return _spawnCounter >= GetEnemyCount() && _cooldownTimer <= 0;
    }

    public void Tick(Bounds bounds)
    {
        Spawnable.Tick();
        _intervalTimer -= Time.deltaTime;
        _cooldownTimer -= Time.deltaTime;
        if (CanSpawn())
        {
            Spawn(bounds);
        }
    }

    public bool CanSpawn()
    {
        return _intervalTimer <= 0 && _spawnCounter < GetEnemyCount();
    }

    public void Spawn(Bounds bounds)
    {
        Spawnable.TrySpawn(Utils.RandomPointInBounds(bounds));
        _spawnCounter++;
        _intervalTimer = SpawnerInterval;
        _cooldownTimer = SpawnerCooldown;
    }
    
    public virtual void NextWave()
    {
        Reset();
    }

    public virtual int GetEnemyCount()
    {
        return SpawnAmount;
    }
}
