using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scriptable object used to describe default and required fields for spawnable objects
/// </summary>
[CreateAssetMenu(fileName = "new Spawnable", menuName = "Spawnables/Spawnable")]
public class ASpawnable : ScriptableObject
{
    [Tooltip("The prefab to spawn when using this spawnable")]
    public GameObject Prefab = default;

    [Tooltip("How much time should there be in between spawning this")]
    public float Cooldown = 1;
    protected float cooldownTimer = 0;

    /// <summary>
    /// Tries to spawn the spawnable
    /// </summary>
    /// <param name="SpawnPos">At what position to spawn the spawnable</param>
    public void TrySpawn(Vector3 spawnPos, Quaternion spawnRot = default)
    {
        if (spawnRot.Equals(default(Quaternion)))
            spawnRot = Quaternion.identity;

        if (CanBeSpawned())
        {
            Spawn(spawnPos, spawnRot);
            cooldownTimer = 0;
        }
    }

    /// <summary>
    /// Spawns an instance of the spawnable
    /// </summary>
    /// /// <param name="SpawnPos">At what position to spawn the spawnable</param>
    protected virtual void Spawn(Vector3 spawnPos, Quaternion spawnRot)
    {
        GameObject gO = Instantiate(Prefab, spawnPos, spawnRot);
        gO.transform.SetParent(CharactersParent.Instance.transform);
    }

    /// <summary>
    /// Check whether the spawnable is ready
    /// </summary>
    protected virtual bool CanBeSpawned()
    {
        return cooldownTimer >= Cooldown;
    }

    /// <summary>
    /// Update function for this spawnable, should be called in Update
    /// </summary>
    public virtual void Tick()
    {
        cooldownTimer += Time.deltaTime;
    }
}