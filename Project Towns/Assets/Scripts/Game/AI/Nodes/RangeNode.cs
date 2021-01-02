using UnityEngine;

public class RangeNode : Node
{
    private float range;
    private Transform target;
    private Transform origin;

    public RangeNode(float range, Transform target, Transform origin)
    {
        this.range = range;
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(target.position, origin.position);
        _nodeState = (distance <= range) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}