using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipbookSpawner : BaseSpawner
{
    private int _spawningIdx = 0;
    private BaseSpawningBehaviour _currentSpawnBehaviour = default;

    // Start is called before the first frame update
    void Start()
    {
        SetSpawnBehaviour();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetSpawnBehaviour()
    {
        if (_spawningIdx >= spawnBehaviours.Count)
        {
            _spawningIdx = 0;
        }

        if (spawnBehaviours.Count > 0)
        {
            _currentSpawnBehaviour = spawnBehaviours[_spawningIdx];
            _currentSpawnBehaviour.Reset();
        }
    }

    protected override void SpawnTick()
    {
        if (_currentSpawnBehaviour)
        {
            // Tick with the currentSpawnBehaviour
            _currentSpawnBehaviour.Tick(spawnArea.bounds);

            // If it's done spawning we go to the next behaviour
            // TODO: Maybe change this to all behaviours running at the same time to create varied waves?
            if (_currentSpawnBehaviour.IsDone())
            {
                _spawningIdx++;
                SetSpawnBehaviour();
            }
        }
    }
}
