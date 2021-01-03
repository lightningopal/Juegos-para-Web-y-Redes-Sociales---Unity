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

        // Si se encuentra en idle, no hace nada
        if (!villagerAnimator.GetBool("idle")){
            villagerAnimator.SetBool("run", false);
            villagerAnimator.SetBool("walk", false);
            villagerAnimator.SetBool("idle", true);
        }

        return NodeState.SUCCESS;
    }
}
