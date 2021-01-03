using UnityEngine;

public class ZoneTimerNode : Node
{
    private Villager villager;

    public ZoneTimerNode(Villager villager_)
    {
        this.villager = villager_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = (villager.timeToNextZone >= Time.time) ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}
