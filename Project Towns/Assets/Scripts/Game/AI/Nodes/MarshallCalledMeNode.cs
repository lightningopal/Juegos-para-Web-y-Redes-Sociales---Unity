public class MarshallCalledMeNode : Node
{
    private NPC npc;

    public MarshallCalledMeNode(NPC npc)
    {
        this.npc = npc;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (npc.hasBeenCalledByMarshall) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}
