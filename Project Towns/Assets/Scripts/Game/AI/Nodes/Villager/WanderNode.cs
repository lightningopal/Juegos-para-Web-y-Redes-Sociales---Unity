using UnityEngine;

public class WanderNode : Node
{
    private Villager villager;

    public WanderNode(Villager villager_)
    {
        this.villager = villager_;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("WanderNode");
        // Si ha llegado al punto deseado, cambia de dirección
        if (villager.thisAgent.remainingDistance <= villager.MINIMUM_DESTINY_DISTANCE)
        {
            // Se cambia la dirección
            Vector3 villagerPosition = villager.transform.position;
            float randomXChange = Random.Range(-villager.WANDER_RADIUS, villager.WANDER_RADIUS);
            float randomZChange = Random.Range(-villager.WANDER_RADIUS, villager.WANDER_RADIUS);

            Vector3 newDestination = new Vector3(villagerPosition.x + randomXChange, villagerPosition.y, villagerPosition.z + randomZChange);
            villager.thisAgent.SetDestination(newDestination);
        }

        // Return SUCCESS
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
