using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Attack Tower", menuName = "Spawnables/AttackTower")]
public class AnAttackTower : ATower
{
    [Header("Tower: Attack Properties")]

    [Tooltip("The attack used by the tower")]
    public ASpawnable TowerAttack = default;

    [Tooltip("The attack range of the tower")]
    public float AttackRange = 0;

    [Tooltip("How fast the turret can take aim")]
    public float RotationSpeed = 1f;

    [Tooltip("What this tower wants to target")]
    public LayerMask TargetMask = default;
}

public class AnAttackTowerLogic : ATowerLogic
{
    public AnAttackTowerLogic(ASpawnable spawnable) : base(spawnable)
    {
    }

    protected override void Spawn(Vector3 spawnPos, Quaternion spawnRot)
    {
        Instantiate(spawnable.Prefab, MouseFollow.Instance.transform.position, Quaternion.identity).GetComponentInChildren<BaseAttackTower>()?.Setup(spawnable as AnAttackTower);
    }
}
