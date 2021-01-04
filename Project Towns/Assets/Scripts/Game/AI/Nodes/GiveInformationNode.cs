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
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
