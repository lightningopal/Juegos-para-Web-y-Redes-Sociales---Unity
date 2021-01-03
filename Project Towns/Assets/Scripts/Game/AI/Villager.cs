using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Clase Villager, que controla a los aldeanos de la partida
/// </summary>
public class Villager : NPC
{
    #region Variables
    [Header("Probabilidades")]
    [Tooltip("Probabilidad de que ambos datos sean seguros siendo víctima")]
    public int victimSafeProbability = 50;
    [Tooltip("Probabilidad de que ambos datos sean veraces siendo víctima")]
    public int victimVeracityProbability = 50;
    [Tooltip("Probabilidad de que el dato sea seguro siendo testigo")]
    public int witnessSafeProbability = 50;
    [Tooltip("Probabilidad de que el dato sea veraz siendo testigo")]
    public int witnessVeracityProbability = 50;

    [Header("Parámetros")]
    [Tooltip("Distancia de visión (cono)")]
    [SerializeField]
    private float visionDistance = 0;
    [Tooltip("Ángulo de visión (cono)")]
    [SerializeField]
    private float visionAngle = 0;

    [Header("Información sobre el aldeano")]
    [Tooltip("Booleano que indica si es víctima")]
    ////[HideInInspector]
    public bool isVictim = false;

    [Header("Zonas")]
    [Tooltip("Tiempo para cambiar de zona")]
    //[HideInInspector]
    public float timeToNextZone = 0.0f;
    [Tooltip("Tiempo que se lleva en una zona")]
    public float timeToChangeZone = 30.0f;
    

    [Header("Velocidades")]
    [Tooltip("Probabilidad de que el aldeano vaya corriendo hacia la zona elegida")]
    public float SPEED_RUN_PROBABILITY = 30.0f;

    [Header("Referencias a otros personajes")]
    [Tooltip("Referencia al ladrón")]
    private Transform thiefTransform;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    protected override void Start()
    {
        // Inicializar NPC
        base.Start();

        // Referencia al ladrón
        //thiefTransform = FindObjectOfType<Thief>().transform;

        // Elegir destino más cercano
        float distanceToNearestZone = float.PositiveInfinity;
        foreach (Zone z in GameManager.instance.zones)
        {
            float distanceToZone = Vector3.Distance(z.enterPoint.position, this.transform.position);
            if (distanceToZone < distanceToNearestZone)
            {
                distanceToNearestZone = distanceToZone;
                destinationZone = z;
                thisAgent.SetDestination(z.enterPoint.position);
            }
        }

        // Comprobar si colisiona con la zona, que significaría que está en la zona
        bool inZone = false;
        Collider[] overlapingColliders = Physics.OverlapSphere(this.transform.position, 1f);
        foreach (Collider c in overlapingColliders)
        {
            if (c.gameObject.CompareTag("Zone"))
            {
                inZone = true;
                actualZone = destinationZone;
                actualZone.villagerCount++;
                destinationZone = null;
                thisAgent.speed = WALKING_SPEED;
                thisAgent.areaMask = (int)Mathf.Pow(2, NavMesh.GetAreaFromName("Zone"));
            }
        }

        // Si no está en ninguna zona, elige un destino aleatorio
        if (!inZone)
        {
            // Se elige una nueva zona
            int randomZoneNumber = Random.Range(0, GameManager.instance.zones.Count);
            Zone newZone = GameManager.instance.zones[randomZoneNumber];

            // Se establece el destino
            destinationZone = newZone;
            thisAgent.SetDestination(newZone.enterPoint.position);

            // Se elige si va andando o corriendo
            int randomSpeedProbability = Random.Range(0, 100);

            if (randomSpeedProbability < SPEED_RUN_PROBABILITY)
                thisAgent.speed = RUNNING_SPEED;
            else
                thisAgent.speed = WALKING_SPEED;
        }

        // Establecer tiempo para cambiar de zona
        timeToNextZone = Time.time + timeToChangeZone;
    }

    /// <summary>
    /// Método OnDrawGizmosSelected, que dibuja gizmos en la escena al seleccionar el personaje
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionDistance);

        Gizmos.color = Color.gray;

        if (thiefTransform != null)
            Gizmos.DrawLine(transform.position, transform.position +
                (thiefTransform.position - transform.position).normalized * visionDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.AngleAxis(visionAngle / 2, Vector3.up) * (transform.forward)).normalized * visionDistance);
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.AngleAxis(-visionAngle / 2, Vector3.up) * (transform.forward)).normalized * visionDistance);
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método CreateBehaviourTree, que crea el árbol de comportamiento
    /// </summary>
    public override void CreateBehaviourTree()
    {
        // Cuarta rama
        MoveToDestinationNode moveToDestinationNode = new MoveToDestinationNode();

        WanderNode wanderNode = new WanderNode(this);
        EnoughSpaceNode enoughSpaceNode = new EnoughSpaceNode(this);
        Sequence sequence6 = new Sequence(new List<Node>() { enoughSpaceNode, wanderNode });

        ChooseDestinationNode chooseDestinationNode = new ChooseDestinationNode(this);
        Selector selector3 = new Selector(new List<Node>() { sequence6, chooseDestinationNode });

        InDestinationNode inDestinationNode = new InDestinationNode(this, MINIMUM_DESTINY_DISTANCE);
        Sequence sequence5 = new Sequence(new List<Node>() { inDestinationNode, selector3 });

        Selector moveSelector = new Selector(new List<Node>() { sequence5, moveToDestinationNode });

        // Tercera rama
        HasDestinationNode hasDestinationNode = new HasDestinationNode(this);
        Inverter destinationInvertedNode = new Inverter(hasDestinationNode);
        Sequence chooseDestinationSequence = new Sequence(new List<Node>() { destinationInvertedNode, chooseDestinationNode });

        // Segunda rama
        ZoneTimerNode zoneTimerNode = new ZoneTimerNode(this);
        InZoneNode inZoneNode = new InZoneNode(this);
        Sequence wanderZoneSequence = new Sequence(new List<Node>() { inZoneNode, zoneTimerNode, wanderNode });

        // Primera rama
        StayStillNode stayStillNode = new StayStillNode(thisAgent, thisAnimator);
        GiveInformationNode giveInformationNode = new GiveInformationNode(this);
        RangeNode marshallInRange = new RangeNode(marshallRange, playerTransform, this.transform);
        Sequence sequence3 = new Sequence(new List<Node>() { marshallInRange, giveInformationNode, stayStillNode });

        HideInformationNode hideInformationNode = new HideInformationNode(this);
        Inverter marshallRangeInvertedNode = new Inverter(marshallInRange);
        HasGivenInformatioNode hasGivenInformatioNode = new HasGivenInformatioNode(this);
        Sequence sequence2 = new Sequence(new List<Node>() { hasGivenInformatioNode, marshallRangeInvertedNode, hideInformationNode });

        Selector selector2 = new Selector(new List<Node>() { sequence2, sequence3 });

        WitnessNode witnessNode = new WitnessNode(this);
        Sequence sequence4 = new Sequence(new List<Node>() { witnessNode, selector2 });

        Selector selector1 = new Selector(new List<Node>() { sequence2, sequence3, stayStillNode });

        VictimNode victimNode = new VictimNode(this);
        Sequence sequence1 = new Sequence(new List<Node>() { victimNode, selector1 });

        Selector staySelector = new Selector(new List<Node>() { sequence1, sequence4 });

        // Nodo padre del árbol
        topNode = new Selector(new List<Node>() { staySelector, wanderZoneSequence, chooseDestinationSequence, moveSelector });
    }

    /// <summary>
    /// Método GetRobbed, para cuando roban al aldeano
    /// </summary>
    private void GetRobbed()
    {
        // Generamos un número aleatorio
        int randomNumber = Random.Range(0, 100);

        // Sigo mañana
    }

    /// <summary>
    /// Método SeeRobbery, que se llama cuando el aldeano ha visto un robo
    /// </summary>
    private void SeeRobbery()
    {
        // Generamos un número aleatorio
        int randomNumber = Random.Range(0, 100);

        // Sigo mañana
    }

    /// <summary>
    /// Método CheckSawRobbery, que comprueba si ha visto el robo
    /// </summary>
    public void CheckSawRobbery()
    {
        // Si lo ha visto, llama al método ver robo
        if (HasSeenRobbery())
        {
            SeeRobbery();
        }
    }

    /// <summary>
    /// Método HasSeenRobbery, para comprobar si ha visto el robo
    /// </summary>
    /// <returns>Booleano que indica si ha visto el robo</returns>
    private bool HasSeenRobbery()
    {
        // Si el ladrón no está en el ángulo, no lo ve
        if (Vector3.Angle(transform.forward.normalized,
            (thiefTransform.position - transform.position).normalized) > visionAngle / 2)
        {
            return false;
        }  

        // Lanzamos rayos para saber si lo ve
        RaycastHit[] hits = Physics.RaycastAll(transform.position,
            (thiefTransform.position - transform.position).normalized, visionDistance);

        // Ordenamos los choques
        System.Array.Sort(hits, delegate (RaycastHit x, RaycastHit y) {
            return x.distance.CompareTo(y.distance);
        });

        // Por cada choque comprobamos si ha chocado con el ladrón
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("thief"))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Método ShowInformation, que muestra la información sobre los objetos
    /// </summary>
    public new void ShowInformation()
    {
        if (!informationGameObject.activeSelf)
        {
            informationGameObject.SetActive(true);
            hasGivenInformation = true;
        }
    }

    /// <summary>
    /// Método HideInformation, que esconde la información sobre los objetos
    /// </summary>
    public new void HideInformation()
    {
        base.HideInformation();
        if (informationGameObject.activeSelf)
        {
            isVictim = false;
        }
    }
    #endregion
}
