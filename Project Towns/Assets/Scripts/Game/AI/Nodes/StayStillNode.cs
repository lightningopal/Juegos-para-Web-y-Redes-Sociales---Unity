using UnityEngine;
using UnityEngine.AI;

public class StayStillNode : Node
{
    private NavMeshAgent agent;
    private Animator villagerAnimator;

    public StayStillNode(NavMeshAgent agent_, Animator villagerAnimator_)
    {
        this.agent = agent_;
        villagerAnimator = villagerAnimator_;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("StayStillNode");
        // Quedarse quieto
        if (agent.hasPath)
            agent.ResetPath();

        villagerAnimator.SetTrigger("idle");

        return NodeState.SUCCESS;
    }
}
