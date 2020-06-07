using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : BaseEnemy
{
    // Interesting explode thing
    protected override void Attack()
    {
        Die();
    }
}
