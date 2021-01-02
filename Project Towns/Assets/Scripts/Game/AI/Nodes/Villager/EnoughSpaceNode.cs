using UnityEngine;
using UnityEngine.AI;
public class EnoughSpaceNode : Node
{
    private Villager villager;

    public EnoughSpaceNode(Villager villager_)
    {
        this.villager = villager_;
    }

    public override NodeState Evaluate()
    {
        // Establecer el tiempo para esa zona
        villager.timeToNextZone = Time.time + villager.timeToChangeZone; 

        // Establecer el destino
        villager.actualZone = villager.destinationZone;

        // Se establece la máscara de zona
        villager.thisAgent.areaMask = NavMesh.GetAreaFromName("Zone");

        _nodeState = (villager.destinationZone.villagerCount < villager.destinationZone.maxVillagers) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}

