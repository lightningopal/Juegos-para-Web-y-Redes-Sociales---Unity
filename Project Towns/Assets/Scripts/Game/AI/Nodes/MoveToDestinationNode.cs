using UnityEngine;
public class MoveToDestinationNode : Node
{
    private NPC npc;

    public MoveToDestinationNode(NPC npc_)
    {
        this.npc = npc_;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("MoveToDestinationNode");
        if (!npc.thisAgent.hasPath)
            npc.thisAgent.SetDestination(npc.destinationZone.enterPoint.position);
        if (npc.isRunning)
        {
            npc.thisAnimator.SetTrigger("run");
        }
        else
        {
            npc.thisAnimator.SetTrigger("walk");
        }

        // Return SUCCESS
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
