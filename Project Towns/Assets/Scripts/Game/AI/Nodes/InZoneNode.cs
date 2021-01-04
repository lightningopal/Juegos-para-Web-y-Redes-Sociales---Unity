using UnityEngine;
public class InZoneNode : Node
{
    private NPC npc;

    public InZoneNode(NPC npc_)
    {
        this.npc = npc_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = NodeState.FAILURE;

        if (npc.actualZone != null)
        {
            if (npc.actualZone.zoneName != null)
            {
                if (npc.actualZone.zoneName != "" &&
                    npc.actualZone.zoneName != string.Empty &&
                    !npc.actualZone.zoneName.Equals(""))
                {
                    _nodeState = NodeState.SUCCESS;
                }
            }
        }

        //Debug.Log("In Zone Node: " + _nodeState);

        return _nodeState;
    }
}
