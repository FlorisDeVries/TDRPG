using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroTransmitter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The base value for this transmitter aggro")]
    private float _aggroValue = 1f;

    [SerializeField]
    [Tooltip("The range that the aggro reaches")]
    private float _aggroRange = 5f;

    private void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Aggro");
        SphereCollider col = this.gameObject.AddComponent<SphereCollider>();
        col.radius = _aggroRange;
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        AggroController controller = other.gameObject.GetComponentInChildren<AggroController>();
        if (controller)
        {
            controller.AddAggroTarget(this, transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AggroController controller = other.gameObject.GetComponentInChildren<AggroController>();
        if (controller)
        {
            controller.RemoveAggroTarget(this);
        }
    }

    public float GetAggroValue(Transform target)
    {
        return _aggroValue;
    }

    void OnDrawGizmosSelected()
    {
        // Debug gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _aggroRange);
    }
}
