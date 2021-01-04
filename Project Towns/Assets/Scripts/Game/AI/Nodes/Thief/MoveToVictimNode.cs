public class MoveToVictimNode : Node
{
    private Thief thief;

    public MoveToVictimNode(Thief thief_)
    {
        this.thief = thief_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = NodeState.FAILURE;
        if (thief.victim != null)
        {
            thief.thisAgent.SetDestination(thief.victim.transform.position);
            _nodeState = NodeState.SUCCESS;
        }

        return _nodeState;
    }
}
