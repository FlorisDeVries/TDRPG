using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that holds all the commonly used prefabs
/// </summary>
public class PrefabManager : UnitySingleton<PrefabManager>
{
    [Header("In-Scene UI")]
    [SerializeField]
    [Tooltip("The prefab used to display floating text")]
    private GameObject _floatingText = default;
    [HideInInspector]
    public GameObject FloatingText { get { return _floatingText; } }
}
