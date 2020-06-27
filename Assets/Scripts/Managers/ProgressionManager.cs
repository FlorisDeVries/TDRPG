using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager
{
    private static ProgressionManager _instance;

    public static ProgressionManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ProgressionManager();
            return _instance;
        }
    }

    public List<AnAttack> UnlockedAttacks { get; private set; }

    private ProgressionManager()
    {
        UnlockedAttacks = new List<AnAttack>();
    }

    public void UnlockAttack(AnAttack _attack)
    {
        UnlockedAttacks.Add(_attack);
    }
}
