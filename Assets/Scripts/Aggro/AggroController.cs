using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controlls what targets give this entity aggro and can be used to find the most aggoed target
/// </summary>
public class AggroController : MonoBehaviour
{
    // Dictionary used to keep track of all aggroTargets that influence this enemy
    private Dictionary<AggroTransmitter, Transform> _aggroTargets = new Dictionary<AggroTransmitter, Transform>();

    // Dictionary to add additional aggro modifier values
    private Dictionary<AggroTransmitter, float> _aggroModifier = new Dictionary<AggroTransmitter, float>();

    [SerializeField]
    List<AggroTransmitter> targets = new List<AggroTransmitter>();

    public void AddAggroTarget(AggroTransmitter target, Transform targetTransform)
    {
        _aggroTargets.Add(target, targetTransform);
        _aggroModifier.Add(target, 0f);

        targets.Add(target);
    }

    public void RemoveAggroTarget(AggroTransmitter target)
    {
        _aggroTargets.Remove(target);
        _aggroModifier.Remove(target);

        targets.Remove(target);
    }

    public Transform GetHighestAggro()
    {
        Transform highest = null;
        float highestValue = float.MinValue;

        List<AggroTransmitter> toRemove = new List<AggroTransmitter>();

        foreach (KeyValuePair<AggroTransmitter, Transform> pair in _aggroTargets)
        {
            if (pair.Value == null)
            {
                toRemove.Add(pair.Key);
                continue;
            }
            if (pair.Key.GetAggroValue(this.transform) + _aggroModifier[pair.Key] > highestValue)
            {
                highest = pair.Value.transform;
                highestValue = pair.Key.GetAggroValue(this.transform);
            }
        }

        foreach (AggroTransmitter remove in toRemove)
        {
            _aggroTargets.Remove(remove);
        }

        return highest;
    }
}
