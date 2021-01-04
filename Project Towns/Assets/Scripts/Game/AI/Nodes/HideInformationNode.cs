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
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
