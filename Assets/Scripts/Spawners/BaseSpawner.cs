using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField]
    private List<BaseSpawningBehaviour> _spawnEntities = new List<BaseSpawningBehaviour>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

