using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scripteable used to determine what enemies are going to spawn this wave
/// </summary>
[CreateAssetMenu(fileName = "new Wave", menuName = "SpawningBehaviour/WaveController")]
public class AWave : ScriptableObject
{
    public List<BaseSpawningBehaviour> SpawningBehaviours;

    public int GetEnemyCount()
    {
        int count = 0;
        foreach (BaseSpawningBehaviour behaviour in SpawningBehaviours)
        {
            count += behaviour.GetEnemyCount();
        }
        return count;
    }

    public void NextWave()
    {
        foreach (BaseSpawningBehaviour behaviour in SpawningBehaviours)
        {
            behaviour.NextWave();
        }
    }

    public void Reset()
    {
        foreach (BaseSpawningBehaviour behaviour in SpawningBehaviours)
        {
            behaviour.Reset();
        }
    }
}
