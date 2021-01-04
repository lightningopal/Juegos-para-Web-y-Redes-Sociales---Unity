using UnityEngine;
public class InDestinationNode : Node
{
    private NPC npc;
    private float minDistance;

    public InDestinationNode(NPC npc_, float minDistance_)
    {
        this.npc = npc_;
        this.minDistance = minDistance_;
    }

    public override NodeState Evaluate()
    {
        float distanceToDestination = float.PositiveInfinity;
        if (npc.destinationZone != null)
        {
            distanceToDestination = Vector3.Distance(npc.transform.position, npc.destinationZone.enterPoint.position);
        }
         
        _nodeState = (distanceToDestination <= minDistance) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}

