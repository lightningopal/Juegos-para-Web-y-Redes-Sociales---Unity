public class HasGivenInformatioNode : Node
{
    private Villager villager;

    public HasGivenInformatioNode(Villager villager_)
    {
        this.villager = villager_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (villager.hasGivenInformation) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}