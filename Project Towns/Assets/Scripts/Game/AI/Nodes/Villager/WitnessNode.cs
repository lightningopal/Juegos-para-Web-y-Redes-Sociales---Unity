public class WitnessNode : Node
{
    private Villager villager;

    public WitnessNode(Villager villager_)
    {
        this.villager = villager_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (villager.isWitness) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}