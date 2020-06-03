using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    [SerializeField]
    [Tooltip("What attack should be pickup up")]
    private AnAttack _pickUp = default;

    private void OnTriggerEnter(Collider other)
    {
        PlayerCombat player = other.gameObject.GetComponentInChildren<PlayerCombat>();
        if (player)
        {
            GivePickUp(player);
        }
    }

    protected virtual void GivePickUp(PlayerCombat player)
    {
        player.AddAttack(_pickUp);
        Destroy(gameObject);
    }
}
