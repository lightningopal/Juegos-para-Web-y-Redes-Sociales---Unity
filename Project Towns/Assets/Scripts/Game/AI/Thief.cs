using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Clase Thief, que controla al ladrón
/// </summary>
public class Thief : NPC
{
    #region Variables
    [Header("Probabilidades")]
    [Tooltip("Probabilidad de que se haga pasar por testigo")]
    [HideInInspector]
    public int fakeWitnessProbability = 50;

    [Header("Tiempos")]
    [Tooltip("Tiempo para el siguiente robo")]
    [HideInInspector]
    public float timeNextSteal = 0.0f;
    [Tooltip("Tiempo entre robos (segundos)")]
    [HideInInspector]
    public float timeBetweenSteals = 30.0f; // Modificable por dificultad

    [Header("Parámetros LADRÓN")]
    [Tooltip("Distancia para detectar al marshall")]
    [SerializeField]
    private float marshallDetectRange = 25.0f;
    [Tooltip("Velocidad de movimiento al robar")]
    public float STEALING_SPEED = 10.0f;
    [Tooltip("Distancia mínima para robar, es decir, para asegurar que está al lado de la víctima")]
    public float MINIMUM_STEAL_DISTANCE = 2f;
    [Tooltip("Rango para saber si hay aldeanos cerca")]
    public float CLOSE_VILLAGERS_RANGE = 30f;

    [Header("Robos")]
    [Tooltip("Referencia a la víctima")]
    [HideInInspector]
    public Villager victim = null;
    [Tooltip("Aldeanos cerca")]
    [HideInInspector]
    public List<Villager> villagersInRange = new List<Villager>();
    [Tooltip("Zona previa")]
    [HideInInspector]
    public Zone previousZone = null;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    protected override void Start()
    {
        // Inicializar NPC
        base.Start();

        // Obtener parámetros
        timeBetweenSteals = GameManager.instance.difficulty.timeBetweenSteals;
        SPEED_RUN_PROBABILITY = GameManager.instance.difficulty.THIEF_SPEED_RUN_PROBABILITY;
        fakeWitnessProbability = GameManager.instance.difficulty.fakeWitnessProbability;

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
                isRunning = false;
                thisAnimator.SetTrigger("walk");
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
            {
                thisAgent.speed = RUNNING_SPEED;
                isRunning = true;
                thisAnimator.SetTrigger("run");
            }
            else
            {
                thisAgent.speed = WALKING_SPEED;
                isRunning = false;
                thisAnimator.SetTrigger("walk");
            }       
        }

        // Establecer tiempo para cambiar de zona
        timeToNextZone = Time.time + timeToChangeZone;

        // Establecertiempo para robar
        timeNextSteal = Time.time + timeBetweenSteals;
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método CreateBehaviourTree, que crea el árbol de comportamiento
    /// </summary>
    public override void CreateBehaviourTree()
    {
        // Sexta rama
        MoveToDestinationNode moveToDestinationNode = new MoveToDestinationNode(this);

        WanderNode wanderNode = new WanderNode(this, this.stillTime);
        EnoughSpaceNode enoughSpaceNode = new EnoughSpaceNode(this);
        Sequence sequence4 = new Sequence(new List<Node>() { enoughSpaceNode, wanderNode});

        ThiefChooseDestinationNoe chooseDestinationNode = new ThiefChooseDestinationNoe(this);
        Selector selector2 = new Selector(new List<Node>() { sequence4, chooseDestinationNode });

        InDestinationNode inDestinationNode = new InDestinationNode(this, MINIMUM_DESTINY_DISTANCE);
        Sequence sequence3 = new Sequence(new List<Node>() { inDestinationNode, selector2 });

        Selector moveSelector = new Selector(new List<Node>() { sequence3, moveToDestinationNode });

        // Quinta rama
        HasDestinationNode hasDestinationNode = new HasDestinationNode(this);
        Inverter destinationInvertedNode = new Inverter(hasDestinationNode);
        Sequence chooseDestinationSequence = new Sequence(new List<Node>() { destinationInvertedNode, chooseDestinationNode });

        // Cuarta rama
        ZoneTimerNode zoneTimerNode = new ZoneTimerNode(this);
        InZoneNode inZoneNode = new InZoneNode(this);
        Sequence wanderZoneSequence = new Sequence(new List<Node>() { inZoneNode, zoneTimerNode, wanderNode });

        // Tercera rama
        ChooseVictimNode chooseVictimNode = new ChooseVictimNode(this);
        VillagersCloseNode villagersCloseNode = new VillagersCloseNode(this);
        Sequence chooseVictimSequence = new Sequence(new List<Node>() { inZoneNode, villagersCloseNode, chooseVictimNode });

        MoveToVictimNode moveToVictimNode = new MoveToVictimNode(this);
        HasVictimNode hasVictimNode = new HasVictimNode(this);
        Sequence chaseVictimSequence = new Sequence(new List<Node>() { hasVictimNode, moveToVictimNode });

        ChooseIfWitnessNode chooseIfWitnessNode = new ChooseIfWitnessNode(this);
        StealNode stealNode = new StealNode(this);
        VictimCloseNode victimCloseNode = new VictimCloseNode(this);
        Sequence stealSequence = new Sequence(new List<Node>() { hasVictimNode, victimCloseNode, stealNode, chooseIfWitnessNode, chooseDestinationNode });

        Selector stealActionsSelector = new Selector(new List<Node>() { stealSequence, chaseVictimSequence, chooseVictimSequence });
        RangeNode marshallInDetectRange = new RangeNode(marshallDetectRange, playerTransform, this.transform);
        Inverter marshallDetectRangeInvertedNode = new Inverter(marshallInDetectRange);
        CanStealNode canStealNode = new CanStealNode(this);
        Sequence robberySequence = new Sequence(new List<Node>() { canStealNode, marshallDetectRangeInvertedNode, stealActionsSelector });

        // Segunda rama
        StayStillNode stayStillNode = new StayStillNode(thisAgent, thisAnimator);
        GiveInformationNode giveInformationNode = new GiveInformationNode(this);
        RangeNode marshallInInfoRange = new RangeNode(marshallInfoRange, playerTransform, this.transform);
        Sequence sequence2 = new Sequence(new List<Node>() { marshallInInfoRange, giveInformationNode, stayStillNode });

        HideInformationNode hideInformationNode = new HideInformationNode(this);
        Inverter marshallInvertedInfoRange = new Inverter(marshallInInfoRange);
        HasGivenInformatioNode hasGivenInformatioNode = new HasGivenInformatioNode(this);
        Sequence sequence1 = new Sequence(new List<Node>() { hasGivenInformatioNode, marshallInvertedInfoRange, hideInformationNode });

        Selector selector1 = new Selector(new List<Node>() { sequence1, sequence2 });

        WitnessNode witnessNode = new WitnessNode(this);
        Sequence fakeWitnessSequence = new Sequence(new List<Node>() { witnessNode, selector1 });

        // Primera rama
        MarshallCalledMeNode marshallCalledMeNode = new MarshallCalledMeNode(this);
        Sequence detentionSequence = new Sequence(new List<Node>() { marshallCalledMeNode, stayStillNode });

        // Nodo padre del árbol
        topNode = new Selector(new List<Node>() { detentionSequence, fakeWitnessSequence, robberySequence, wanderZoneSequence, chooseDestinationSequence, moveSelector });
    }

    /// <summary>
    /// Método CalculateFakeItem, que se llama cuando comete un robo
    /// </summary>
    public void CalculateFakeItem()
    {
        // Instanciamos el efecto que avisa al jugador
        if (infoVFX != null)
        {
            // Para que no se repitan
            Destroy(infoVFX);
        }
        infoVFX = Instantiate(GameManager.instance.witnessVFX, transform);
     
        // Ponemos el item2 a null
        informationGameObject.item2 = null;

        // Generamos un número aleatorio, que representa el item que finge saber
        int randomNumber = Random.Range(0, 5);

        Item item;
        bool isSameItem = false;
        bool fakeIsNoItem = false;

        // Obtenemos el objeto falso
        switch (randomNumber)
        {
            case 0:
                // Color
                // Calculamos el color falso
                MaterialItem materialItem;
                do
                {
                    int randomItemNumber = Random.Range(0, ItemDatabase.instance.characterColors.Length);
                    materialItem = ItemDatabase.instance.characterColors[randomItemNumber];
                } while (materialItem.itemName == this.items.villagerColor.itemName);

                // Lo asignamos al objeto de información
                this.informationGameObject.item1 = materialItem;
                this.informationGameObject.item1Sprite.sprite = materialItem.itemSprite;

                break;
            case 1:
                // Número de ojos
                // Calculamos el número falso de ojos
                int eyesNumber;
                do
                {
                    eyesNumber = Random.Range(1, 4);
                    item = ItemDatabase.instance.eyes[eyesNumber - 1];
                } while (eyesNumber == this.items.eyes.eyesNumber);

                // Lo asignamos al objeto de información
                this.informationGameObject.item1 = item;
                this.informationGameObject.item1Sprite.sprite = item.itemSprite;

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
                        item = ItemDatabase.instance.hatItems[randomItemNumber];
                        if (this.items.hatItem != null)
                            if (item.itemName == this.items.hatItem.itemName)
                                isSameItem = true;
                    }
                    else
                    {
                        item = ItemDatabase.instance.noItems[0];
                        if (this.items.hatItem.itemName == item.itemName)
                            isSameItem = true;
                        else
                            fakeIsNoItem = true;
                    }
                } while (isSameItem);

                // Lo asignamos al objeto de información
                if (fakeIsNoItem)
                    this.informationGameObject.item1 = ItemDatabase.instance.noItems[0];
                else
                    this.informationGameObject.item1 = item;
                this.informationGameObject.item1Sprite.sprite = item.itemSprite;

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
                        item = ItemDatabase.instance.hornItems[randomItemNumber];
                        if (this.items.hornItem != null)
                            if (item.itemName == this.items.hornItem.itemName)
                                isSameItem = true;
                    }
                    else
                    {
                        item = ItemDatabase.instance.noItems[1];
                        if (this.items.hornItem.itemName == item.itemName)
                            isSameItem = true;
                        else
                            fakeIsNoItem = true;
                    }
                } while (isSameItem);

                // Lo asignamos al objeto de información
                if (fakeIsNoItem)
                    this.informationGameObject.item1 = ItemDatabase.instance.noItems[1];
                else
                    this.informationGameObject.item1 = item;
                this.informationGameObject.item1Sprite.sprite = item.itemSprite;

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
                        item = ItemDatabase.instance.neckItems[randomItemNumber];
                        if (this.items.neckItem != null)
                            if (item.itemName == this.items.neckItem.itemName)
                                isSameItem = true;
                    }
                    else
                    {
                        item = ItemDatabase.instance.noItems[2];
                        if (this.items.neckItem.itemName == item.itemName)
                            isSameItem = true;
                        else
                            fakeIsNoItem = true;
                    }
                } while (isSameItem);

                // Lo asignamos al objeto de información
                if (fakeIsNoItem)
                    this.informationGameObject.item1 = ItemDatabase.instance.noItems[2];
                else
                    this.informationGameObject.item1 = item;
                this.informationGameObject.item1Sprite.sprite = item.itemSprite;

                informationGameObject.doubtfulInformation = true;
                //informationGameObject.CalculateInformation();

                break;
        }
    }
    #endregion
}