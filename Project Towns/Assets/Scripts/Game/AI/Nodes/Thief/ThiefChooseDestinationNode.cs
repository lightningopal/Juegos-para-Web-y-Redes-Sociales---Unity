using UnityEngine;
using UnityEngine.AI;
public class ThiefChooseDestinationNoe : Node
{
    private Thief thief;

    public ThiefChooseDestinationNoe(Thief thief_)
    {
        this.thief = thief_;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("ChooseDestinationNode");
        // Se elige una nueva zona
        int randomZoneNumber;
        Zone newZone = GameManager.instance.zones[0]; ;

        // Comparamos que no sea la misma
        if (thief.actualZone != null)
        {
            do
            {
                randomZoneNumber = Random.Range(0, GameManager.instance.zones.Count);
                newZone = GameManager.instance.zones[randomZoneNumber];
            } while (newZone.zoneName == thief.actualZone.zoneName);

            thief.actualZone.villagerCount--;
            thief.actualZone = null;
        }

        // Comparamos que no sea la previa
        if (thief.previousZone != null)
        {
            do
            {
                randomZoneNumber = Random.Range(0, GameManager.instance.zones.Count);
                newZone = GameManager.instance.zones[randomZoneNumber];
            } while (newZone.zoneName == thief.previousZone.zoneName);

            thief.previousZone = null;
        }

        // Se establece la máscara para todas las áreas
        thief.thisAgent.areaMask = NavMesh.AllAreas;

        // Se establece el destino
        thief.destinationZone = newZone;
        thief.thisAgent.SetDestination(newZone.enterPoint.position);

        // Se elige si va andando o corriendo
        int randomSpeedProbability = Random.Range(0, 100);
        
        if (randomSpeedProbability < thief.SPEED_RUN_PROBABILITY)
        {
            thief.thisAgent.speed = thief.RUNNING_SPEED;
            thief.isRunning = true;
            thief.thisAnimator.SetTrigger("run");
        }
        else
        {
            thief.thisAgent.speed = thief.WALKING_SPEED;
            thief.isRunning = false;
            thief.thisAnimator.SetTrigger("walk");
        }

        // Si tenía víctima, ya no jeje
        if (thief.victim != null)
            thief.victim = null;

        // Devolvemos SUCCESS
        _nodeState = NodeState.SUCCESS;
        return _nodeState;
    }
}
