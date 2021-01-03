using UnityEngine;
public class HasDestinationNode : Node
{
    private Villager villager;

    public HasDestinationNode(Villager villager)
    {
        this.villager = villager;
    }

    public override NodeState Evaluate()
    {
        _nodeState = NodeState.FAILURE;

        if (villager.destinationZone != null)
        {
            if (villager.destinationZone.zoneName != null)
            {
                if (villager.destinationZone.zoneName != "" &&
                    villager.destinationZone.zoneName != string.Empty &&
                    !villager.destinationZone.zoneName.Equals(""))
                {
                    _nodeState = NodeState.SUCCESS;
                }
            }
        }
        //Debug.Log("Has Destination Node: " + _nodeState);

        return _nodeState;
    }
}
