using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    [SerializeField]
    [Tooltip("What attack should be pickup up")]
    private AnAttack _pickUp = default;

    [SerializeField]
    [Tooltip("Whether picking this up should add to the rewards")]
    private bool _addToRewards = false;

    private void OnTriggerEnter(Collider other)
    {
        PlayerCombat player = other.gameObject.GetComponentInChildren<PlayerCombat>();
        if (player)
        {
            GivePickUp(player);
        }

        if (_addToRewards)
            EndLevelPortal.Instance.AddReward(_pickUp);
    }
    protected virtual void GivePickUp(PlayerCombat player)
    {
        player.AddAttack(_pickUp);
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(.1f, .5f, -.01f));
    }
}
