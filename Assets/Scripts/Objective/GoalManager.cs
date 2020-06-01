using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoalManager : UnitySingleton<GoalManager>
{
    [SerializeField]
    private List<Goal> _goals = new List<Goal>();

    // Start is called before the first frame update
    protected override void Awake() {
        base.Awake();
        _goals = GetComponentsInChildren<Goal>().ToList();
    }

    public Goal GetClosestGoal(Vector3 pos)
    {
        Goal closest = null;
        float distance = float.MaxValue;

        foreach (Goal goal in _goals)
        {
            if (goal && Vector3.Distance(goal.transform.position, pos) < distance)
            {
                closest = goal;
                distance = Vector3.Distance(goal.transform.position, pos);
            }
        }

        return closest;
    }
}
