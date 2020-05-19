using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class that instantiates a given gameobject on destroy
/// </summary>
public class InstantiateOnDestroy : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The gameobject to spawn")]
    GameObject _toSpawn = default;

    private void OnDestroy() {
        Instantiate(_toSpawn, transform.position, Quaternion.identity);
    }
}
