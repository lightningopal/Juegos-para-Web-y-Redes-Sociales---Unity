using UnityEngine;
using UnityEngine.AI;
public class EnoughSpaceNode : Node
{
    private NPC npc;
    private Animator npcAnimator;

    public EnoughSpaceNode(NPC npc_)
    {
        this.npc = npc_;
        npcAnimator = npc.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("EnoughSpaceNode");
        // Si hay hueco
        if (npc.destinationZone.villagerCount < npc.destinationZone.maxVillagers)
        {
            // Establecer el tiempo para esa zona
            npc.timeToNextZone = Time.time + npc.timeToChangeZone;

            // Establecer la zona
            npc.actualZone = npc.destinationZone;
            npc.actualZone.villagerCount++;
            npc.destinationZone = null;

            // Establecer la velocidad
            npc.thisAgent.speed = npc.WALKING_SPEED;
            npc.isRunning = false;
            npcAnimator.SetTrigger("walk");

            // Se establece la máscara de zona
            npc.thisAgent.areaMask = (int)Mathf.Pow(2, NavMesh.GetAreaFromName("Zone"));

            _nodeState = NodeState.SUCCESS;
        }
        else
            _nodeState = NodeState.FAILURE;

        return _nodeState;
    }
}

