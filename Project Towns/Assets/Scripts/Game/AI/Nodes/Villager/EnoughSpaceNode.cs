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
        // Si hay hueco
        if (villager.destinationZone.villagerCount < villager.destinationZone.maxVillagers)
        {
            // Establecer el tiempo para esa zona
            Debug.Log("EnoughSpaceNode");
            villager.timeToNextZone = Time.time + villager.timeToChangeZone;

            // Establecer el destino
            villager.actualZone = villager.destinationZone;

            // Se establece la máscara de zona
            villager.thisAgent.areaMask = (int)Mathf.Pow(2, NavMesh.GetAreaFromName("Zone"));

            _nodeState = NodeState.SUCCESS;
        }
        else
            _nodeState = NodeState.FAILURE;

        return _nodeState;
    }
}

