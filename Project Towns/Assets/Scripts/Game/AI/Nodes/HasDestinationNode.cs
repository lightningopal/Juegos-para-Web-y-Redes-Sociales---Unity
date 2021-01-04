using UnityEngine;
public class HasDestinationNode : Node
{
    private NPC npc;

    public HasDestinationNode(NPC npc)
    {
        this.npc = npc;
    }

    public override NodeState Evaluate()
    {
        _nodeState = NodeState.FAILURE;

        if (npc.destinationZone != null)
        {
            if (npc.destinationZone.zoneName != null)
            {
                if (npc.destinationZone.zoneName != "" &&
                    npc.destinationZone.zoneName != string.Empty &&
                    !npc.destinationZone.zoneName.Equals(""))
                {
                    _nodeState = NodeState.SUCCESS;
                }
            }
        }
        //Debug.Log("Has Destination Node: " + _nodeState);

        return _nodeState;
    }
}
