using UnityEngine;
public class HideInformationNode : Node
{
    private NPC npc;

    public HideInformationNode(NPC npc_)
    {
        this.npc = npc_;
    }

    public override NodeState Evaluate()
    {
        npc.HideInformation();
        if (npc.destinationZone != null)
            npc.thisAgent.SetDestination(npc.destinationZone.enterPoint.position);
        if (npc.isRunning)
        {
            npc.thisAnimator.SetTrigger("run");
        }
        else
        {
            npc.thisAnimator.SetTrigger("walk");
        }
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
