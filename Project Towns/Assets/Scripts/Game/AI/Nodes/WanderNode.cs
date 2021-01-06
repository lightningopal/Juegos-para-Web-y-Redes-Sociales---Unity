using UnityEngine;

public class WanderNode : Node
{
    private NPC npc;

    public WanderNode(NPC npc_)
    {
        this.npc = npc_;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("WanderNode");
        // Si ha llegado al punto deseado, cambia de dirección
        if (npc.thisAgent.remainingDistance <= npc.MINIMUM_DESTINY_DISTANCE)
        {
            // Se cambia la dirección
            Vector3 npcPosition = npc.transform.position;
            float randomXChange = Random.Range(-npc.WANDER_RADIUS, npc.WANDER_RADIUS);
            float randomZChange = Random.Range(-npc.WANDER_RADIUS, npc.WANDER_RADIUS);

            Vector3 newDestination = new Vector3(npcPosition.x + randomXChange, npcPosition.y, npcPosition.z + randomZChange);
            npc.thisAgent.SetDestination(newDestination);
        }
        if (npc.isRunning)
        {
            npc.thisAnimator.SetTrigger("run");
        }
        else
        {
            npc.thisAnimator.SetTrigger("walk");
        }

        // Return SUCCESS
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
