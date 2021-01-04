using UnityEngine;

public class VictimCloseNode : Node
{
    private Thief thief;

    public VictimCloseNode(Thief thief_)
    {
        this.thief = thief_;
    }

    public override NodeState Evaluate()
    {
        _nodeState = NodeState.FAILURE;
        if (thief.victim != null)
        {
            float distanceToVictim = Vector3.Distance(thief.transform.position, thief.victim.transform.position);
            if (distanceToVictim <= thief.MINIMUM_STEAL_DISTANCE)
            {
                _nodeState = NodeState.SUCCESS;
            }
        }
        return _nodeState;
    }
}
