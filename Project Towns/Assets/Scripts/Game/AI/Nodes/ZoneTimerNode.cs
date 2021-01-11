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
        if (npc.timeToNextZone >= Time.time)
        {
            _nodeState = NodeState.SUCCESS;
        }
        else
        {
            _nodeState = NodeState.FAILURE;
            // Cuando cambia de zona, esconde el bocadillo de estado
            npc.HideEmoji();
        }
        //if (_nodeState == NodeState.FAILURE)
        //Debug.Log("Zone Timer Node: FAILURE");
        return _nodeState;
    }
}
