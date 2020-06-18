using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    private BoxCollider _collider = default;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        EndlessWaveManager.Instance.AddSpawner(this);
    }

    public void Spawn(GameObject prefab)
    {
        Vector3 pos = Utils.RandomPointInBounds(_collider.bounds);
        Instantiate(prefab, pos, Quaternion.identity).transform.SetParent(CharactersParent.Instance.transform);
    }
}

