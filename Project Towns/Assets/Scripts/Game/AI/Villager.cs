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
    [Tooltip("Probabilidad de que ambos datos sean seguros siendo víctima (2 verdad SIN ?)")]
    [HideInInspector]
    public float victimSafeProbability = 50; // Modificable por dificultad
    [Tooltip("Probabilidad de que ambos datos sean veraces siendo víctima (2 verdad CON ?)")]
    [HideInInspector]
    public float victimVeracityProbability = 50; // Modificable por dificultad
    [Tooltip("Probabilidad de que el dato sea seguro siendo testigo (1 verdad SIN ?)")]
    [HideInInspector]
    public float witnessSafeProbability = 50; // Modificable por dificultad
    [Tooltip("Probabilidad de que el dato sea veraz siendo testigo (1 verdad CON ?)")]
    [HideInInspector]
    public float witnessVeracityProbability = 50; // Modificable por dificultad

    [Header("Parámetros ALDEANO")]
    [Tooltip("Distancia de visión (cono)")]
    [SerializeField]
    private float visionDistance = 0;
    [Tooltip("Ángulo de visión (cono)")]
    [SerializeField]
    private float visionAngle = 0;

    [Header("Referencias a otros personajes")]
    [Tooltip("Referencia al ladrón")]
    private Thief thief;
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
        thief = FindObjectOfType<Thief>();

        // Obtener parámetros
        victimSafeProbability = GameManager.instance.difficulty.victimSafeProbability;
        victimVeracityProbability = GameManager.instance.difficulty.victimVeracityProbability;
        witnessSafeProbability = GameManager.instance.difficulty.witnessSafeProbability;
        witnessVeracityProbability = GameManager.instance.difficulty.witnessVeracityProbability;
        SPEED_RUN_PROBABILITY = GameManager.instance.difficulty.VILLAGER_SPEED_RUN_PROBABILITY;

        // Calcular destino más cercano
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

        if (thief != null)
            if (thief.transform != null)
                Gizmos.DrawLine(transform.position, transform.position +
                    (thief.transform.position - transform.position).normalized * visionDistance);

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
        // Quinta rama
        MoveToDestinationNode moveToDestinationNode = new MoveToDestinationNode(this);

        WanderNode wanderNode = new WanderNode(this);
        EnoughSpaceNode enoughSpaceNode = new EnoughSpaceNode(this);
        Sequence sequence6 = new Sequence(new List<Node>() { enoughSpaceNode, wanderNode });

        VillagerChooseDestinationNode chooseDestinationNode = new VillagerChooseDestinationNode(this);
        Selector selector3 = new Selector(new List<Node>() { sequence6, chooseDestinationNode });

        InDestinationNode inDestinationNode = new InDestinationNode(this, MINIMUM_DESTINY_DISTANCE);
        Sequence sequence5 = new Sequence(new List<Node>() { inDestinationNode, selector3 });

        Selector moveSelector = new Selector(new List<Node>() { sequence5, moveToDestinationNode });

        // Cuarta rama
        HasDestinationNode hasDestinationNode = new HasDestinationNode(this);
        Inverter destinationInvertedNode = new Inverter(hasDestinationNode);
        Sequence chooseDestinationSequence = new Sequence(new List<Node>() { destinationInvertedNode, chooseDestinationNode });

        // Tercera rama
        ZoneTimerNode zoneTimerNode = new ZoneTimerNode(this);
        InZoneNode inZoneNode = new InZoneNode(this);
        Sequence wanderZoneSequence = new Sequence(new List<Node>() { inZoneNode, zoneTimerNode, wanderNode });

        // Segunda rama
        StayStillNode stayStillNode = new StayStillNode(thisAgent, thisAnimator);
        GiveInformationNode giveInformationNode = new GiveInformationNode(this);
        RangeNode marshallInRange = new RangeNode(marshallInfoRange, playerTransform, this.transform);
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

        // Primera rama
        MarshallCalledMeNode marshallCalledMeNode = new MarshallCalledMeNode(this);
        Sequence detentionSequence = new Sequence(new List<Node>() { marshallCalledMeNode, stayStillNode });

        // Nodo padre del árbol
        topNode = new Selector(new List<Node>() { detentionSequence, staySelector, wanderZoneSequence, chooseDestinationSequence, moveSelector });
    }

    /// <summary>
    /// Método ChooseTrueItem, que devuelve un objeto del ladrón
    /// </summary>
    /// <returns>Objeto que lleva el ladrón</returns>
    public Item ChooseTrueItem()
    {
        Item trueItem = new Item();

        // Generamos un número aleatorio, que representa el item a elegir
        int randomItemNumber = Random.Range(0, 5);

        switch (randomItemNumber)
        {
            case 0:
                // Color
                trueItem = thief.items.villagerColor;
                break;
            case 1:
                // Número de ojos
                trueItem = thief.items.eyes;
                break;
            case 2:
                // Sombrero
                trueItem = thief.items.hatItem;
                break;
            case 3:
                // Cuernos
                trueItem = thief.items.hornItem;
                break;
            case 4:
                // Accesorio del cuello
                trueItem = thief.items.neckItem;
                break;
        }

        return trueItem;
    }

    /// <summary>
    /// Método ChooseFakeItem, que devuelve un item falso del ladrón
    /// </summary>
    /// <returns>Item falso</returns>
    public Item ChooseFakeItem()
    {
        Item fakeItem = new Item();

        // Generamos un número aleatorio, que representa el item que da falso
        int randomNumber = Random.Range(0, 5);

        bool isSameItem = false;

        // Obtenemos el objeto falso
        switch (randomNumber)
        {
            case 0:
                // Color
                // Calculamos el color falso
                do
                {
                    int randomItemNumber = Random.Range(0, ItemDatabase.instance.characterColors.Length);
                    fakeItem = ItemDatabase.instance.characterColors[randomItemNumber];
                } while (fakeItem.itemName == thief.items.villagerColor.itemName);

                break;
            case 1:
                // Número de ojos
                // Calculamos el número falso de ojos
                int eyesNumber;
                do
                {
                    eyesNumber = Random.Range(1, 4);
                    fakeItem = ItemDatabase.instance.eyes[eyesNumber - 1];
                } while (eyesNumber == thief.items.eyes.eyesNumber);

                break;
            case 2:
                // Sombrero
                // Calculamos el sombrero falso
                do
                {
                    isSameItem = false;
                    int randomItemNumber = Random.Range(-1, ItemDatabase.instance.hatItems.Count);
                    if (randomItemNumber != -1)
                    {
                        fakeItem = ItemDatabase.instance.hatItems[randomItemNumber];
                        if (thief.items.hatItem != null)
                            if (fakeItem.itemName == thief.items.hatItem.itemName)
                                isSameItem = true;
                    }
                    else
                    {
                        fakeItem = ItemDatabase.instance.noItems[0];
                        if (thief.items.hatItem.itemName == fakeItem.itemName)
                            isSameItem = true;
                        else
                            fakeItem = ItemDatabase.instance.noItems[0];
                    }
                } while (isSameItem);

                break;
            case 3:
                // Cuernos
                // Calculamos los cuernos falsos
                do
                {
                    isSameItem = false;
                    int randomItemNumber = Random.Range(-1, ItemDatabase.instance.hornItems.Count);
                    if (randomItemNumber != -1)
                    {
                        fakeItem = ItemDatabase.instance.hornItems[randomItemNumber];
                        if (thief.items.hornItem != null)
                            if (fakeItem.itemName == thief.items.hornItem.itemName)
                                isSameItem = true;
                    }
                    else
                    {
                        fakeItem = ItemDatabase.instance.noItems[1];
                        if (thief.items.hornItem.itemName == fakeItem.itemName)
                            isSameItem = true;
                        else
                            fakeItem = ItemDatabase.instance.noItems[1];
                    }
                } while (isSameItem);

                break;
            case 4:
                // Accesorio de cuello
                // Calculamos el accesorio de cuello falso
                do
                {
                    isSameItem = false;
                    int randomItemNumber = Random.Range(-1, ItemDatabase.instance.neckItems.Count);
                    if (randomItemNumber != -1)
                    {
                        fakeItem = ItemDatabase.instance.neckItems[randomItemNumber];
                        if (thief.items.neckItem != null)
                            if (fakeItem.itemName == thief.items.neckItem.itemName)
                                isSameItem = true;
                    }
                    else
                    {
                        fakeItem = ItemDatabase.instance.noItems[2];
                        if (thief.items.neckItem.itemName == fakeItem.itemName)
                            if (thief.items.neckItem == null)
                            isSameItem = true;
                        else
                            fakeItem = ItemDatabase.instance.noItems[2];
                    }
                } while (isSameItem);

                break;
        }

        return fakeItem;
    }

    /// <summary>
    /// Método GetRobbed, para cuando roban al aldeano
    /// </summary>
    public void GetRobbed()
    {
        // Establecemos que es víctima
        isVictim = true;

        // Generamos un número aleatorio
        int randomSafeNumber = Random.Range(0, 100);

        // Elegimos el objeto seguro
        informationGameObject.item1 = ChooseTrueItem();

        // Si ambos objetos son seguros
        if (randomSafeNumber < victimSafeProbability)
        {
            informationGameObject.doubtfulInformation = false;

            // Comprobamos que el segundo no sea el mismo que el primero
            do
            {
                informationGameObject.item2 = ChooseTrueItem();
            } while (informationGameObject.item1.itemName == informationGameObject.item2.itemName);

        }
        // Si un objeto es seguro y el otro no
        else
        {
            informationGameObject.doubtfulInformation = true;

            // Generamos un número aleatorio
            int randomVeracityNumber = Random.Range(0, 100);

            // Si el objeto no seguro es verdadero
            if (randomVeracityNumber < victimVeracityProbability)
            {
                // Comprobamos que el segundo no sea el mismo que el primero
                do
                {
                    informationGameObject.item2 = ChooseTrueItem();
                } while (informationGameObject.item1.itemName == informationGameObject.item2.itemName);
            }
            // Si el objeto no seguro es falso
            else
            {
                informationGameObject.item2 = ChooseFakeItem();

                Item randomItem1 = informationGameObject.item1;
                Item randomItem2 = informationGameObject.item2;

                Item[] randomItems = { randomItem1, randomItem2 };

                int randomFirstItem = Random.Range(0, 2);
                informationGameObject.item1 = randomItems[randomFirstItem];
                informationGameObject.item2 = (randomFirstItem == 0) ? randomItems[1] : randomItems[0];
            }
        }

        // Calculamos información
        informationGameObject.CalculateInformation();
    }

    /// <summary>
    /// Método SeeRobbery, que se llama cuando el aldeano ha visto un robo
    /// </summary>
    private void SeeRobbery()
    {
        // Establecemos que es testigo
        isWitness = true;
        informationGameObject.item2 = null;

        // Generamos un número aleatorio
        int randomSafeNumber = Random.Range(0, 100);

        // Si el objeto es seguro
        if (randomSafeNumber < witnessSafeProbability)
        {
            informationGameObject.doubtfulInformation = false;

            informationGameObject.item1 = ChooseTrueItem();
        }
        // Si el objeto no es seguro
        else
        {
            informationGameObject.doubtfulInformation = true;

            // Generamos un número aleatorio
            int randomVeracityNumber = Random.Range(0, 100);

            // Si el objeto es verdadero
            if (randomVeracityNumber < witnessVeracityProbability)
            {
                informationGameObject.item1 = ChooseTrueItem();
            }
            // Si el objeto es falso
            else
            {
                informationGameObject.item1 = ChooseFakeItem();
            }
        }

        // Calculamos información
        informationGameObject.CalculateInformation();
    }

    /// <summary>
    /// Método CheckSawRobbery, que comprueba si ha visto el robo
    /// </summary>
    public void CheckSawRobbery()
    {
        // Si no es víctima y lo ha visto, llama al método ver robo
        if (!isVictim)
        {
            if (HasSeenRobbery())
            {
                SeeRobbery();
            }
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
            (thief.transform.position - transform.position).normalized) > visionAngle / 2)
        {
            return false;
        }  

        // Lanzamos rayos para saber si lo ve
        RaycastHit[] hits = Physics.RaycastAll(transform.position,
            (thief.transform.position - transform.position).normalized, visionDistance);

        // Ordenamos los choques
        System.Array.Sort(hits, delegate (RaycastHit x, RaycastHit y) {
            return x.distance.CompareTo(y.distance);
        });

        // Por cada choque comprobamos si ha chocado con el ladrón
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Thief"))
                return true;
        }
        return false;
    }
    #endregion
}
