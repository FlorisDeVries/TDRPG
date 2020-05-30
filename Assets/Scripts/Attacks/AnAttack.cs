using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scriptable object used to describe default and required fields for attacks
/// </summary>
[CreateAssetMenu(fileName = "new Attack", menuName = "Spawnables/Attack")]
public class AnAttack : ScriptableObject
{
    [Header("Attack Properties")]
    [Tooltip("Image/Icon used to display this attack in the hotbar")]
    public Sprite Image = default;

    [Tooltip("The prefab to spawn when using this attack")]
    public GameObject Attack = default;

    [Tooltip("How much time should there be in between firing this attack")]
    public float Cooldown = 0;
    private float _cooldownTimer = 0;

    private HotbarItem _hotbarItem = default;

    public void Fire(Transform firePoint)
    {
        if (CanAttack())
        {
            SpawnAttack(firePoint);
            _cooldownTimer = 0;
        }
    }

    public void SetupHotbar(Transform parent, GameObject prefab)
    {
        _hotbarItem = Instantiate(prefab).GetComponent<HotbarItem>();
        _hotbarItem.transform.SetParent(parent);
        _hotbarItem.SetImage(Image);
    }

    protected virtual void SpawnAttack(Transform firePoint)
    {
        Instantiate(Attack, firePoint.position, firePoint.rotation);
        CameraShake.Instance.Shake(Random.Range(.01f, .05f));
    }

    public void SetSelected(bool selected)
    {
        _hotbarItem.SetSelected(selected);
    }

    protected virtual bool CanAttack()
    {
        return _cooldownTimer >= Cooldown;
    }

    public virtual void Tick()
    {
        _cooldownTimer += Time.deltaTime;
        _hotbarItem.SetCooldownProgress(Mathf.Min(1, _cooldownTimer / Cooldown));
    }
}
