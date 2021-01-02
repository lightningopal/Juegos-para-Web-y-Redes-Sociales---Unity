public class GiveInformationNode : Node
{
    private Villager villager;

    public GiveInformationNode(Villager villager_)
    {
        this.villager = villager_;
    }

    public override NodeState Evaluate()
    {
        villager.ShowInformation();
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
