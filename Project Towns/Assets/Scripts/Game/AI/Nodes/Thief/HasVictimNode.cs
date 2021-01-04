public class HasVictimNode : Node
{
    private Thief thief;

    public HasVictimNode(Thief thief_)
    {
        this.thief = thief_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (thief.victim != null) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}
