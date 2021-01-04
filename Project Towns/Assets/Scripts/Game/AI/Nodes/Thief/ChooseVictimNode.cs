using System.Collections.Generic;
using UnityEngine;
public class ChooseVictimNode : Node
{
    private Thief thief;

    public ChooseVictimNode(Thief thief_)
    {
        this.thief = thief_;
    }

    public override NodeState Evaluate()
    {
        // Si hay aldeanos en rango, se elige una víctima
        if (thief.villagersInRange.Count > 0)
        {
            int randomVictimNumber = Random.Range(0, thief.villagersInRange.Count);
            Villager victim = thief.villagersInRange[randomVictimNumber];
            thief.victim = victim;
        }

        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
