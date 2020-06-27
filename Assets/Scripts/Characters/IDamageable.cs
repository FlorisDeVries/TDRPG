using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// Called when this entity gets hit
    /// </summary>
    /// <param name="damage">How many damage is inflicted</param>
    /// <param name="position">Where were we hit</param>
    /// <param name="direction">In what direction where we hit</param>
    void GetHit(float damage, Vector3 position, Vector3 direction);

    /// <summary>
    /// Called when this entity gets hit
    /// </summary>
    /// <param name="damage">How many damage is inflicted</param>
    /// <param name="position">Where were we hit</param>
    /// <param name="direction">In what direction where we hit</param>
    /// <param name="transmitter">What aggro transmitter hit us</param>
    void GetHit(float damage, Vector3 position, Vector3 direction, AggroTransmitter transmitter);
}