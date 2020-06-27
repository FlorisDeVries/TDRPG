using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tower", menuName = "Spawnables/Tower")]
public class ATower : AnAttack
{
    [Header("Tower Properties")]
    [Tooltip("How much currency is required to build this tower.")]
    public float Cost = 10f;

    [Tooltip("How much health points this tower will have.")]
    public float HealtPoints = 10f;
}

/// <summary>
/// Wrapper class to keep instances and settings separate
/// </summary>
public class ATowerLogic : AnAttackLogic
{
    public ATowerLogic(ASpawnable spawnable) : base(spawnable)
    {

    }

    protected override void Spawn(Vector3 spawnPos, Quaternion spawnRot)
    {
        Instantiate(spawnable.Prefab, MouseFollow.Instance.transform.position, Quaternion.identity).GetComponentInChildren<BaseTower>()?.Setup(spawnable as ATower);
    }

    protected override bool CanBeSpawned()
    {
        // TODO: Check whether we can actually place this tower with munnie
        return (spawnable as ATower).Cost > 0 && MouseFollow.Instance.PlaceableGround && base.CanBeSpawned();
    }
}
