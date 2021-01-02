public class InDestinationNode : Node
{
    private Villager villager;
    private float minDistance;

    public InDestinationNode(Villager villager_, float minDistance_)
    {
        this.villager = villager_;
        this.minDistance = minDistance_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (villager.thisAgent.remainingDistance <= minDistance) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}

