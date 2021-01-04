using UnityEngine;

public class ZoneTimerNode : Node
{
    private NPC npc;

    public ZoneTimerNode(NPC npc_)
    {
        this.npc = npc_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (npc.timeToNextZone >= Time.time) ? NodeState.SUCCESS : NodeState.FAILURE;
        //if (_nodeState == NodeState.FAILURE)
        //Debug.Log("Zone Timer Node: FAILURE");
        return _nodeState;
    }
}
