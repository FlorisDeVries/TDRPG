using System.Collections;
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
    public Sprite Image = default;

    private HotbarItem _hotbarItem = default;


    public void SetupAttack(Transform parent, GameObject prefab)
    {
        SetupHotbar(parent, prefab);
        SetSelected(false);
    }


    public void SetupHotbar(Transform parent, GameObject prefab)
    {
        _hotbarItem = Instantiate(prefab).GetComponent<HotbarItem>();
        _hotbarItem.transform.SetParent(parent);
        _hotbarItem.SetImage(Image);
    }

    public void SetSelected(bool selected)
    {
        _hotbarItem.SetSelected(selected);
    }

    protected override void Spawn(Transform spawnPos)
    {
        base.Spawn(spawnPos);
        CameraShake.Instance.Shake(Random.Range(.01f, .05f));
    }

    public override void Tick()
    {
        base.Tick();
        _hotbarItem.SetCooldownProgress(Mathf.Min(1, cooldownTimer / Cooldown));
    }
}
