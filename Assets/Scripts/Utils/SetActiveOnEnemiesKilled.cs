using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Small script that responds to the all dead event to set a gameObject to active
/// </summary>
public class SetActiveOnEnemiesKilled : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Whether the gameObject should be set to active or inactive")]
    private bool _setActive = true;

    void Start()
    {
        EnemyManager.Instance.OnAllDead.AddListener(() => gameObject.SetActive(_setActive));
        gameObject.SetActive(!_setActive);
    }
}
