using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Clase Villager, que controla a los aldeanos de la partida
/// </summary>
public class Villager : MonoBehaviour
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
    [Tooltip("Distancia de visión")]
    [SerializeField]
    private float visionDistance = 0;
    [Tooltip("Ángulo de visión")]
    [SerializeField]
    private float visionAngle = 0;
    [Tooltip("Distancia para el marshall")]
    [SerializeField]
    private float marshallRange = 3.0f;

    [Header("Información sobre el aldeano")]
    [Tooltip("Booleano que indica si es víctima")]
    ////[HideInInspector]
    public bool isVictim = false;
    [Tooltip("Booleano que indica si es testigo")]
    //[HideInInspector]
    public bool isWitness = false;
    [Tooltip("Booleano que indica si ha dado información")]
    //[HideInInspector]
    public bool hasGivenInformation = false;

    [Header("Zonas")]
    [Tooltip("Zona actual")]
    //[HideInInspector]
    public Zone actualZone = null;
    [Tooltip("Zona destino")]
    //[HideInInspector]
    public Zone destinationZone = null;
    [Tooltip("Tiempo para cambiar de zona")]
    //[HideInInspector]
    public float timeToNextZone = 0.0f;
    [Tooltip("Tiempo que se lleva en una zona")]
    public float timeToChangeZone = 30.0f;
    [Tooltip("Distancia mínima para llegar al destino")]
    public float MINIMUM_DESTINY_DISTANCE = 0.5f;

    [Header("Velocidades")]
    [Tooltip("Probabilidad de que el aldeano vaya corriendo hacia la zona elegida")]
    public float SPEED_RUN_PROBABILITY = 30.0f;
    [Tooltip("Velocidad del aldeano andando")]
    public float WALKING_SPEED = 4.0f;
    [Tooltip("Velocidad del aldeano corriendo")]
    public float RUNNING_SPEED = 7.0f;

    [Header("Movimiento en zona")]
    [Tooltip("Radio del wander")]
    public float WANDER_RADIUS = 2.0f;
    [Tooltip("Tiempo para el siguiente wander")]
    //[HideInInspector]
    public float wanderNextTime = 0.0f;

    [Header("Objetos")]
    [Tooltip("Items del aldeano")]
    public VillagerItems items;
    [Tooltip("Ojos del aldeano")]
    [SerializeField]
    private GameObject[] eyes = new GameObject[3];
    [Tooltip("Padre del sombrero del aldeano")]
    [SerializeField]
    private GameObject hatParent = null;
    [Tooltip("Padre de los cuernos del aldeano")]
    [SerializeField]
    private GameObject hornsParent = null;
    [Tooltip("Padre del objeto del cuello del aldeano")]
    [SerializeField]
    private GameObject neckItemParent = null;

    [Header("Referencias a otros personajes")]
    [Tooltip("Referencia al jugador")]
    private Transform playerTransform;
    [Tooltip("Referencia al ladrón")]
    private Transform thiefTransform;

    [Header("Otros")]
    [Tooltip("GameObject que contiene la información")]
    [SerializeField]
    private GameObject informationGameObject = null;
    [Tooltip("Agente NavMesh")]
    public NavMeshAgent thisAgent;
    [Tooltip("Animator")]
    [SerializeField]
    private Animator thisAnimator = null;

    [Tooltip("Árbol de comportamiento")]
    private Node topNode;

    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        // Aleatorizar aldeano
        RandomizeVillager();

        // Referencia al ladrón
        //thief = FindObjectOfType<PlayerController>().transform;

        // Referencia al jugador
        playerTransform = FindObjectOfType<PlayerController>().transform;

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
                Debug.Log("Nueva zona: " + z.zoneName);
            }
        }

        // Comprobar si colisiona con la zona
        Collider[] overlapingColliders = Physics.OverlapSphere(this.transform.position, 1f);
        foreach (Collider c in overlapingColliders)
        {
            if (c.gameObject.CompareTag("Zone"))
            {
                Debug.Log("Está en zona: " + c.gameObject.name);
                actualZone = destinationZone;
                thisAgent.areaMask = (int)Mathf.Pow(2, NavMesh.GetAreaFromName("Zone"));
            }
        }

        // Crear Árbol
        CreateBehaviourTree();

        // Establecer tiempo para cambiar de zona
        timeToNextZone = Time.time + timeToChangeZone;
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        // Ejecutar el árbol de decisión
        topNode.Evaluate();

        /*if (HasSeenRobbery())
        {
            thisAgent.SetDestination(thief.position);
        }*/
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
    /// Método RandomizeVillager, que aleatoriza los objetos del aldeano
    /// </summary>
    public void RandomizeVillager()
    {
        // Color
        int randomMaterialNumber = Random.Range(0, 5);
        items.villagerColor = ItemDatabase.instance.characterColors[randomMaterialNumber];
        this.GetComponentInChildren<SkinnedMeshRenderer>().material = items.villagerColor.itemMaterial;

        // Ojos
        int randomEyesNumber = Random.Range(1, 4);
        items.eyesNumber = randomEyesNumber;

        // Desactivamos los ojos en desuso
        switch (randomEyesNumber)
        {
            case 1:
                eyes[1].SetActive(false);
                eyes[2].SetActive(false);
                break;
            case 2:
                eyes[0].SetActive(false);
                break;
        }

        // Objetos
        // Sombrero
        int randomHatNumber = Random.Range(-1, ItemDatabase.instance.hatItems.Count);
        if (randomHatNumber == -1)
            items.hatItem = null;
        else
        {
            items.hatItem = ItemDatabase.instance.hatItems[randomHatNumber];
            Instantiate(items.hatItem.itemGameObject, hatParent.transform);
        }

        // Cuernos
        int randomHornsNumber = Random.Range(-1, ItemDatabase.instance.hornItems.Count);
        if (randomHornsNumber == -1)
            items.hornItem = null;
        else
        {
            items.hornItem = ItemDatabase.instance.hornItems[randomHornsNumber];
            Instantiate(items.hornItem.itemGameObject, hornsParent.transform);
        }

        // Objetos del cuello
        int randomNeckItemNumber = Random.Range(-1, ItemDatabase.instance.neckItems.Count);
        if (randomNeckItemNumber == -1)
            items.neckItem = null;
        else
        {
            items.neckItem = ItemDatabase.instance.neckItems[randomNeckItemNumber];
            Instantiate(items.neckItem.itemGameObject, neckItemParent.transform);
        }
    }

    /// <summary>
    /// Método CreateBehaviourTree, que crea el árbol de comportamiento
    /// </summary>
    public void CreateBehaviourTree()
    {
        // Cuarta rama
        MoveToDestinationNode moveToDestinationNode = new MoveToDestinationNode();

        WanderNode wanderNode = new WanderNode(this);
        EnoughSpaceNode enoughSpaceNode = new EnoughSpaceNode(this);
        Sequence sequence6 = new Sequence(new List<Node>() { enoughSpaceNode, wanderNode });

        ChooseDestinationNode chooseDestinationNode = new ChooseDestinationNode(this);
        Selector selector2 = new Selector(new List<Node>() { sequence6, chooseDestinationNode });

        InDestinationNode inDestinationNode = new InDestinationNode(this, MINIMUM_DESTINY_DISTANCE);
        Sequence sequence5 = new Sequence(new List<Node>() { inDestinationNode, selector2 });

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
        Sequence sequence2 = new Sequence(new List<Node>() { marshallInRange, giveInformationNode, stayStillNode });

        HideInformationNode hideInformationNode = new HideInformationNode(this);
        Inverter marshallRangeInvertedNode = new Inverter(marshallInRange);
        HasGivenInformatioNode hasGivenInformatioNode = new HasGivenInformatioNode(this);
        Sequence sequence7 = new Sequence(new List<Node>() { hasGivenInformatioNode, marshallRangeInvertedNode, hideInformationNode });

        Selector selector3 = new Selector(new List<Node>() { sequence7, sequence2 });

        WitnessNode witnessNode = new WitnessNode(this);
        Sequence sequence4 = new Sequence(new List<Node>() { witnessNode, selector3 });

        Selector selector1 = new Selector(new List<Node>() { sequence7, sequence2, stayStillNode });

        VictimNode victimNode = new VictimNode(this);
        Sequence sequence1 = new Sequence(new List<Node>() { victimNode, selector1 });

        Selector staySelector = new Selector(new List<Node>() { sequence1, sequence4 });

        // Nodo padre del árbol
        topNode = new Selector(new List<Node>() { staySelector, wanderZoneSequence, chooseDestinationSequence, moveSelector });
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
    /// Método SeeRobbery, que se llama cuando el aldeano ha visto un robo
    /// </summary>
    private void SeeRobbery()
    {

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
    /// Método ShowInformation, que muestra la información sobre los objetos
    /// </summary>
    public void ShowInformation()
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
    public void HideInformation()
    {
        if (informationGameObject.activeSelf)
        {
            hasGivenInformation = false;
            informationGameObject.SetActive(false);
            isVictim = false;
            isWitness = false;
        }
    }
    #endregion
}
