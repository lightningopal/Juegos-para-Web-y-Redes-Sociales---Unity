using UnityEngine;
public class InZoneNode : Node
{
    private Villager villager;

    public InZoneNode(Villager villager_)
    {
        this.villager = villager_;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("IN ZONE NODE");
        _nodeState = (villager.actualZone.zoneName != "") ? NodeState.SUCCESS : NodeState.FAILURE;
        return _nodeState;
    }
}
