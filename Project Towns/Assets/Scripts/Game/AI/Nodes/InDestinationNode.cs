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
         if (distanceToDestination <= minDistance)
        {
            _nodeState = NodeState.SUCCESS;

            Vector3 npcPosition = npc.transform.position;
            float randomXChange = Random.Range(-npc.WANDER_RADIUS, npc.WANDER_RADIUS);
            float randomZChange = Random.Range(-npc.WANDER_RADIUS, npc.WANDER_RADIUS);

            Vector3 newDestination = new Vector3(npcPosition.x + randomXChange, npcPosition.y, npcPosition.z + randomZChange);
            npc.thisAgent.SetDestination(newDestination);
        }
        else
        {
            _nodeState = NodeState.FAILURE;
        }
        //_nodeState = (distanceToDestination <= minDistance) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}

