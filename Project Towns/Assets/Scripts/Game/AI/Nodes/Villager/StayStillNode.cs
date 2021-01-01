using UnityEngine;
//using UnityEngine.AI;

public class StayStillNode : Node
{

    private Villager villager;
    private Animator villagerAnimator;
    //private NavMeshAgent villagerAgent;

    public StayStillNode(Villager villager_)
    {
        this.villager = villager_;
        villagerAnimator = villager.gameObject.GetComponent<Animator>();
        //villagerAgent = villager.gameObject.GetComponent<NavMeshAgent>();
    }
    // Para la animación de andar o correr y comienza la animación de idle
    public override NodeState Evaluate()
    {
        // NavMeshAgent.clearPath() --> Importante
        // Si se encuentra en idle, no hace nada
        if(!villagerAnimator.GetBool("idle")){
            villagerAnimator.SetBool("run", false);
            villagerAnimator.SetBool("walk", false);
            villagerAnimator.SetBool("idle", true);
        }
        

        return NodeState.SUCCESS;
    }
}
