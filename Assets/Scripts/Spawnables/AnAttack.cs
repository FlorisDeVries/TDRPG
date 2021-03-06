﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object used to describe default and required fields for attack
/// </summary>
[CreateAssetMenu(fileName = "new Attack", menuName = "Spawnables/Attack")]
public class AnAttack : ASpawnable
{
    [Header("Spawn Properties")]
    [Tooltip("Image/Icon used to display this attack in the hotbar")]
    public Sprite Sprite = default;
}

/// <summary>
/// Wrapper class to keep instances and settings separate
/// </summary>
public class AnAttackLogic : ASpawnableLogic
{
    private HotbarItem _hotbarItem = default;
    private AggroTransmitter _transmitter = default;

    public AnAttackLogic(ASpawnable spawnable) : base(spawnable)
    {

    }

    public AnAttackLogic(ASpawnable spawnable, AggroTransmitter transmitter) : base(spawnable)
    {
        _transmitter = transmitter;
    }

    public void SetupAttack(Transform parent, GameObject prefab)
    {
        SetupHotbar(parent, prefab);
        SetSelected(false);
    }


    public void SetupHotbar(Transform parent, GameObject prefab)
    {
        _hotbarItem = Instantiate(prefab).GetComponent<HotbarItem>();
        _hotbarItem.transform.SetParent(parent);
        _hotbarItem.SetImage((spawnable as AnAttack).Sprite);
    }

    public void SetSelected(bool selected)
    {
        _hotbarItem.SetSelected(selected);
    }

    protected override void Spawn(Vector3 spawnPos, Quaternion spawnRot)
    {
        if (_transmitter == null)
        {
            base.Spawn(spawnPos, spawnRot);
            CameraShake.Instance.Shake(Random.Range(.01f, .05f));
        }
        else
        {
            GameObject gO = Instantiate(spawnable.Prefab, spawnPos, spawnRot);
            gO.transform.SetParent(CharactersParent.Instance.transform);
            gO.GetComponent<BaseProjectile>()?.SetupProjectile(_transmitter);
        }
    }

    public override void Tick()
    {
        base.Tick();
        _hotbarItem.SetCooldownProgress(Mathf.Min(1, cooldownTimer / (spawnable as AnAttack).Cooldown));
    }
}