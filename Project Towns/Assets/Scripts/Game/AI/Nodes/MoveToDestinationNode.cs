using UnityEngine;
public class MoveToDestinationNode : Node
{
    public override NodeState Evaluate()
    {
        Debug.Log("MoveToDestinationNode");
        // Return SUCCESS
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
