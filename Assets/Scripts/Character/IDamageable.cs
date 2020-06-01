using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Hit(float damage, Vector3 position, Vector3 direction);    
}