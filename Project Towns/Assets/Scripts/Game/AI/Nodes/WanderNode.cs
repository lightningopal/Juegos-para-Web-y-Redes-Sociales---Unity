using UnityEngine;

public class WanderNode : Node
{
    private NPC npc;

    private float stillTime = 1.0f;
    private float nextChange = 0.0f;
    private bool isMoving = true;
    private Vector3 newDestination;

    public WanderNode(NPC npc_, float stillTime_)
    {
        this.npc = npc_;
        this.stillTime = stillTime_;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("WanderNode");
        if (isMoving)
        {
            // Si ha llegado al punto deseado, se queda quieto
            if (npc.thisAgent.remainingDistance <= 0.5f)
            {
                isMoving = false;
                nextChange = Time.time + stillTime;
            }
            if (npc.isRunning)
            {
                npc.thisAnimator.SetTrigger("run");
            }
            else
            {
                npc.thisAnimator.SetTrigger("walk");
            }
        }
        else
        {
            if (npc.thisAgent.hasPath)
                npc.thisAgent.ResetPath();
            npc.thisAnimator.SetTrigger("idle");
            // Se comprueba el tiempo para volver a moverse
            if (Time.time >= nextChange)
            {
                isMoving = true;
                // Se cambia la dirección
                Vector3 npcPosition = npc.transform.position;
                float randomXChange = Random.Range(-npc.WANDER_RADIUS, npc.WANDER_RADIUS);
                float randomZChange = Random.Range(-npc.WANDER_RADIUS, npc.WANDER_RADIUS);

                newDestination = new Vector3(npcPosition.x + randomXChange, npcPosition.y, npcPosition.z + randomZChange);
                npc.thisAgent.SetDestination(newDestination);
            }
        }

        // Return SUCCESS
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
