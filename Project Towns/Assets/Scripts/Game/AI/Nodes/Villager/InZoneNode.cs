public class InZoneNode : Node
{
    private Villager villager;

    public InZoneNode(Villager villager_)
    {
        this.villager = villager_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (villager.actualZone != null) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}
