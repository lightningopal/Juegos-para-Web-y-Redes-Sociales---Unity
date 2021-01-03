using UnityEngine;
public class InDestinationNode : Node
{
    private Villager villager;
    private float minDistance;

    public InDestinationNode(Villager villager_, float minDistance_)
    {
        this.villager = villager_;
        this.minDistance = minDistance_;
    }

    public override NodeState Evaluate()
    {
        float distanceToDestination = float.PositiveInfinity;
        if (villager.destinationZone != null)
        {
            distanceToDestination = Vector3.Distance(villager.transform.position, villager.destinationZone.enterPoint.position);
        }
         
        _nodeState = (distanceToDestination <= minDistance) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}

