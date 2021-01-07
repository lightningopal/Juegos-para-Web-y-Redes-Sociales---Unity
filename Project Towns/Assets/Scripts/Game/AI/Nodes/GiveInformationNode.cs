using UnityEngine;
public class GiveInformationNode : Node
{
    private NPC npc;

    public GiveInformationNode(NPC npc_)
    {
        this.npc = npc_;
    }

    public override NodeState Evaluate()
    {
        npc.ShowInformation();
        npc.transform.rotation = Quaternion.RotateTowards(npc.transform.rotation, Quaternion.LookRotation(npc.playerTransform.position - npc.transform.position), 10);
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
