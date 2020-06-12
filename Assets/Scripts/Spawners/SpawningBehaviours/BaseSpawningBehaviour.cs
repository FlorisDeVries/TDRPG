using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object used to describe a spawning behaviour used in spawners
/// </summary>
[CreateAssetMenu(fileName = "new SpawningBehaviour", menuName = "SpawningBehavriour/Basic")]
public class BaseSpawningBehaviour : ScriptableObject
{
    [Header("Spawning Properties")]
    [Tooltip("The basic spawnable to be spawned by this spawning behaviour")]
    public ASpawnable Spawnable = default;

    [Tooltip("How many of the spawnable should be spawned before this behaviour is finished")]
    public int SpawnAmount = 5;

    [Tooltip("How long the spawner should wait after spawning this wave")]
    public float SpawnerCooldown = 1f;
    
}
