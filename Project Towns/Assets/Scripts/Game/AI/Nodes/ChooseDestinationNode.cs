using UnityEngine;
using UnityEngine.AI;
public class ChooseDestinationNode : Node
{
    private NPC npc;

    public ChooseDestinationNode(NPC npc_)
    {
        this.npc = npc_;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("ChooseDestinationNode");
        // Se elige una nueva zona
        int randomZoneNumber;
        Zone newZone = GameManager.instance.zones[0]; ;

        if (npc.actualZone != null)
        {
            do
            {
                randomZoneNumber = Random.Range(0, GameManager.instance.zones.Count);
                newZone = GameManager.instance.zones[randomZoneNumber];
            } while (newZone.zoneName == npc.actualZone.zoneName);
        }

        // Sale de la zona actual
        npc.actualZone.villagerCount--;
        npc.actualZone = null;

        // Se establece la máscara para todas las áreas
        npc.thisAgent.areaMask = NavMesh.AllAreas;

        // Se establece el destino
        npc.destinationZone = newZone;
        npc.thisAgent.SetDestination(newZone.enterPoint.position);

        // Se elige si va andando o corriendo
        int randomSpeedProbability = Random.Range(0, 100);
        
        if (randomSpeedProbability < npc.SPEED_RUN_PROBABILITY)
            npc.thisAgent.speed = npc.RUNNING_SPEED;
        else
            npc.thisAgent.speed = npc.WALKING_SPEED;

        // Devolvemos SUCCESS
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
