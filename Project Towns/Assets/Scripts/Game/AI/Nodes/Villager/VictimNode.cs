public class VictimNode : Node
{
    private Villager villager;

    public VictimNode(Villager villager_)
    {
        this.villager = villager_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (villager.isVictim) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}