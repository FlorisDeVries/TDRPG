using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The spawning behaviours associated with this spawner")]
    protected List<BaseSpawningBehaviour> spawnBehaviours = new List<BaseSpawningBehaviour>();

    protected BoxCollider spawnArea = default;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();
        WaveManager.Instance.AddSpawner(this);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTick();
    }

    protected virtual void SpawnTick()
    {
        foreach (BaseSpawningBehaviour behaviour in spawnBehaviours)
        {
            if (!behaviour.IsDone())
                behaviour.Tick(spawnArea.bounds);
        }
    }

    public void SetBehaviours(List<BaseSpawningBehaviour> behaviours)
    {
        spawnBehaviours = behaviours;
    }
}

