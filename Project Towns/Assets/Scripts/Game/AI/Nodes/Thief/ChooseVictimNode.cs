using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

        // Se le permite ir a cualquier zona
        thief.thisAgent.areaMask = NavMesh.AllAreas;

        // Se establece la velocidad de robo
        thief.thisAgent.speed = thief.STEALING_SPEED;

        // Se establecen sus zonas a null
        thief.actualZone = null;
        thief.destinationZone = null;

        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
