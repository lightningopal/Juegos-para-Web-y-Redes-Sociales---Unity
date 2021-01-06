using UnityEngine;
using UnityEngine.AI;
public class VillagerChooseDestinationNode : Node
{
    private Villager villager;

    public VillagerChooseDestinationNode(Villager villager_)
    {
        this.villager = villager_;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("ChooseDestinationNode");
        // Se elige una nueva zona
        int randomZoneNumber;
        Zone newZone = GameManager.instance.zones[0]; ;

        if (villager.actualZone != null)
        {
            do
            {
                randomZoneNumber = Random.Range(0, GameManager.instance.zones.Count);
                newZone = GameManager.instance.zones[randomZoneNumber];
            } while (newZone.zoneName == villager.actualZone.zoneName);

            villager.actualZone.villagerCount--;
            villager.actualZone = null;
        }


        // Se establece la máscara para todas las áreas
        villager.thisAgent.areaMask = NavMesh.AllAreas;

        // Se establece el destino
        villager.destinationZone = newZone;
        villager.thisAgent.SetDestination(newZone.enterPoint.position);

        // Se elige si va andando o corriendo
        int randomSpeedProbability = Random.Range(0, 100);
        
        if (randomSpeedProbability < villager.SPEED_RUN_PROBABILITY)
            villager.thisAgent.speed = villager.RUNNING_SPEED;
        else
            villager.thisAgent.speed = villager.WALKING_SPEED;

        // Devolvemos SUCCESS
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
