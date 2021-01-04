using UnityEngine;
public class StealNode : Node
{
    private Thief thief;

    public StealNode(Thief thief_)
    {
        this.thief = thief_;
    }

    public override NodeState Evaluate()
    {
        // Robamos al aldeano
        thief.victim.GetRobbed();

        // Avisamos a los aldeanos por si hubiera testigos
        foreach (Villager villager in GameManager.instance.NPCs)
        {
            villager.CheckSawRobbery();
        }

        // Calcula items falsos
        thief.CalculateFakeItem();

        // Resetea el tiempo para el siguiente robo
        thief.timeNextSteal = Time.time + thief.timeBetweenSteals;

        // Añadimos un robo a la partida
        GameManager.instance.AddRobbery();

        // Mostramos el robo en la UI
        UIManager.instance.ShowRobberyIcon(thief.transform.position);

        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
