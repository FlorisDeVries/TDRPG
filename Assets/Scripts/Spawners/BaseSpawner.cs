using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The spawning behaviours associated with this spawner")]
    private List<BaseSpawningBehaviour> _spawnBehaviours = new List<BaseSpawningBehaviour>();

    private int _spawningIdx = 0;

    private BaseSpawningBehaviour _currentSpawnBehaviour = default;

    private BoxCollider _collider = default;

    void Start()
    {
        _collider = GetComponent<BoxCollider>();

        SetSpawnBehaviour();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentSpawnBehaviour)
        {
            // Tick with the currentSpawnBehaviour
            _currentSpawnBehaviour.Tick(_collider.bounds);

            // If it's done spawning we go to the next behaviour
            // TODO: Maybe change this to all behaviours running at the same time to create varied waves?
            if (_currentSpawnBehaviour.IsDone())
            {
                _spawningIdx++;
                SetSpawnBehaviour();
            }
        }
    }

    private void SetSpawnBehaviour()
    {
        Debug.Log("Setting spawner");
        if (_spawningIdx >= _spawnBehaviours.Count)
        {
            _spawningIdx = 0;
        }

        if (_spawnBehaviours.Count > 0)
        {
            _currentSpawnBehaviour = _spawnBehaviours[_spawningIdx];
            _currentSpawnBehaviour.Reset();
        }
    }
}

