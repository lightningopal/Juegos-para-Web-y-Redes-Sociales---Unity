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
        _nodeState = NodeState.FAILURE;

        if (villager.actualZone != null)
        {
            if (villager.actualZone.zoneName != null)
            {
                if (villager.actualZone.zoneName != "" &&
                    villager.actualZone.zoneName != string.Empty &&
                    !villager.actualZone.zoneName.Equals(""))
                {
                    _nodeState = NodeState.SUCCESS;
                }
            }
        }

        //Debug.Log("In Zone Node: " + _nodeState);

        return _nodeState;
    }
}
