using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object used to describe a spawning behaviour used in wave spawners
/// </summary>
[CreateAssetMenu(fileName = "new SpawningBehaviour", menuName = "SpawningBehaviour/Wave")]
public class WaveSpawningBehaviour : BaseSpawningBehaviour
{
    private int WaveCount = 0;
    public int Multiplier = 10;


    public override int GetEnemyCount()
    {
        return SpawnAmount + (int)(Multiplier * Mathf.Sqrt(WaveCount));
    }

    public override void NextWave()
    {
        base.NextWave();
        WaveCount++;
    }
}

