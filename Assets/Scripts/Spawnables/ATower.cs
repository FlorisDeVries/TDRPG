using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tower", menuName = "Spawnables/Tower")]
public class ATower : ASpawnable
{
    [Header("Tower Properties")]
    [SerializeField]
    [Tooltip("How much currency is required to build this tower.")]
    private float _cost = 10f;

    [Tooltip("How much health points this tower will have.")]
    public float HealtPoints = 10f;

    protected override void Spawn(Vector3 spawnPos, Quaternion spawnRot)
    {
        Instantiate(Prefab, MouseFollow.Instance.transform.position, Quaternion.identity).GetComponentInChildren<BaseTower>()?.Setup(this);
    }

    protected override bool CanBeSpawned()
    {
        // TODO: Check whether we can actually place this tower with munnie
        return _cost > 0 && MouseFollow.Instance.PlaceableGround && base.CanBeSpawned();
    }
}
