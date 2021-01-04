public class WitnessNode : Node
{
    private NPC npc;

    public WitnessNode(NPC npc_)
    {
        this.npc = npc_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (npc.isWitness) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}