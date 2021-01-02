public class HideInformationNode : Node
{
    private Villager villager;

    public HideInformationNode(Villager villager_)
    {
        this.villager = villager_;
    }

    public override NodeState Evaluate()
    {
        villager.HideInformation();
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
