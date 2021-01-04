public class HasGivenInformatioNode : Node
{
    private NPC npc;

    public HasGivenInformatioNode(NPC npc_)
    {
        this.npc = npc_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (npc.hasGivenInformation) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}