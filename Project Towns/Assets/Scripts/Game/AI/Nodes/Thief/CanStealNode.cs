using UnityEngine;
public class CanStealNode : Node
{
    private Thief thief;

    public CanStealNode(Thief thief_)
    {
        this.thief = thief_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (thief.timeNextSteal <= Time.time) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}
