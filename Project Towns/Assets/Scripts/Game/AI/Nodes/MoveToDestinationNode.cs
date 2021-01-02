public class MoveToDestinationNode : Node
{
    public override NodeState Evaluate()
    {
        // Return SUCCESS
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
