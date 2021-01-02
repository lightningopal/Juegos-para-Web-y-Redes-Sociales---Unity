public class HasDestinationNode : Node
{
    private Villager villager;

    public HasDestinationNode(Villager villager)
    {
        this.villager = villager;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (villager.destinationZone != null) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}
