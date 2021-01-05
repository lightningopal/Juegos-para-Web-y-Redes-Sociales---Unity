using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Clase NPC, que controla a los personajes no jugables
/// </summary>
public class NPC : MonoBehaviour
{
    #region Variables
    [Header("Parámetros")]
    [Tooltip("Distancia para mostrar información al marshall")]
    [SerializeField]
    protected float marshallInfoRange = 3.0f;

    [Header("Información sobre el NPC")]
    [Tooltip("Booleano que indica si es testigo")]
    //[HideInInspector]
    public bool isWitness = false;
    [Tooltip("Booleano que indica si es víctima")]
    ////[HideInInspector]
    public bool isVictim = false;
    [Tooltip("Booleano que indica si ha dado información")]
    //[HideInInspector]
    public bool hasGivenInformation = false;
    [Tooltip("Booleano que indica si ha sido llamado por el marshall")]
    //[HideInInspector]
    public bool hasBeenCalledByMarshall = false;

    [Header("Zonas")]
    [Tooltip("Zona actual")]
    //[HideInInspector]
    public Zone actualZone = null;
    [Tooltip("Zona destino")]
    //[HideInInspector]
    public Zone destinationZone = null;
    [Tooltip("Distancia mínima para llegar al destino")]
    public float MINIMUM_DESTINY_DISTANCE = 0.5f;
    [Tooltip("Tiempo para cambiar de zona")]
    //[HideInInspector]
    public float timeToNextZone = 0.0f;
    [Tooltip("Tiempo que se lleva en una zona")]
    public float timeToChangeZone = 30.0f;

    [Header("Velocidades")]
    [Tooltip("Probabilidad de que el aldeano vaya corriendo hacia la zona elegida")]
    public float SPEED_RUN_PROBABILITY = 30.0f;
    [Tooltip("Velocidad del NPC andando")]
    public float WALKING_SPEED = 2.0f;
    [Tooltip("Velocidad del NPC corriendo")]
    public float RUNNING_SPEED = 4.0f;

    [Header("Movimiento en zona")]
    [Tooltip("Radio del wander")]
    public float WANDER_RADIUS = 2.0f;

    [Header("Objetos")]
    [Tooltip("Items del NPC")]
    public NPCItems items;
    [Tooltip("Ojos del NPC")]
    [SerializeField]
    protected GameObject[] eyes = new GameObject[3];
    [Tooltip("Padre del sombrero del NPC")]
    [SerializeField]
    protected GameObject hatParent = null;
    [Tooltip("Padre de los cuernos del NPC")]
    [SerializeField]
    protected GameObject hornsParent = null;
    [Tooltip("Padre del objeto del cuello del NPC")]
    [SerializeField]
    protected GameObject neckItemParent = null;

    [Header("Referencias a otros personajes")]
    [Tooltip("Referencia al jugador")]
    protected Transform playerTransform;

    [Header("Otros")]
    [Tooltip("GameObject que contiene la información")]
    [SerializeField]
    protected InformationObject informationGameObject = null;
    [Tooltip("Agente NavMesh")]
    public NavMeshAgent thisAgent;
    [Tooltip("Animator")]
    [SerializeField]
    protected Animator thisAnimator = null;

    [Tooltip("Árbol de comportamiento")]
    protected Node topNode;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    protected virtual void Start()
    {
        // Referencia al jugador
        playerTransform = FindObjectOfType<PlayerController>().transform;

        // Crear Árbol
        CreateBehaviourTree();
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        // Ejecutar el árbol de decisión
        topNode.Evaluate();
    }
    #endregion

    #region MétodosClase

    /// <summary>
    /// Método RandomizeNPC, que aleatoriza los objetos del NPC
    /// </summary>
    public void RandomizeNPC()
    {
        // Color
        int randomMaterialNumber = Random.Range(0, 5);
        items.villagerColor = ItemDatabase.instance.characterColors[randomMaterialNumber];
        this.GetComponentInChildren<SkinnedMeshRenderer>().material = items.villagerColor.itemMaterial;

        // Ojos
        int randomEyesNumber = Random.Range(1, 4);
        items.eyes = ItemDatabase.instance.eyes[randomEyesNumber - 1];

        // Desactivamos los ojos en desuso
        for (int i = 0; i < eyes.Length; i++)
        {
            eyes[i].SetActive(true);
        }

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
        // Si ya tiene uno, lo borramos
        items.hatItem = null;
        items.hornItem = null;
        items.neckItem = null;

        // Sombrero
        int randomHatNumber = Random.Range(-1, ItemDatabase.instance.hatItems.Count);
        if (randomHatNumber != -1)
            items.hatItem = ItemDatabase.instance.hatItems[randomHatNumber];
        else
            items.hatItem = ItemDatabase.instance.noItems[0];

        // Cuernos
        int randomHornsNumber = Random.Range(-1, ItemDatabase.instance.hornItems.Count);
        if (randomHornsNumber != -1)
            items.hornItem = ItemDatabase.instance.hornItems[randomHornsNumber];
        else
            items.hornItem = ItemDatabase.instance.noItems[1];

        // Objetos del cuello
        int randomNeckItemNumber = Random.Range(-1, ItemDatabase.instance.neckItems.Count);
        if (randomNeckItemNumber != -1)
            items.neckItem = ItemDatabase.instance.neckItems[randomNeckItemNumber];
        else
            items.neckItem = ItemDatabase.instance.noItems[2];
    }

    /// <summary>
    /// Método PutItems, que instancia los objetos del NPC
    /// </summary>
    public void PutItems()
    {
        if (items.hatItem != null)
            if (items.hatItem.itemName != ItemDatabase.instance.noItems[0].itemName)
                Instantiate(items.hatItem.itemGameObject, hatParent.transform);
                

        if (items.hornItem != null)
            if (items.hornItem.itemName != ItemDatabase.instance.noItems[1].itemName)
                Instantiate(items.hornItem.itemGameObject, hornsParent.transform);

        if (items.neckItem != null)
            if (items.neckItem.itemName != ItemDatabase.instance.noItems[2].itemName)
                Instantiate(items.neckItem.itemGameObject, neckItemParent.transform);
    }

    /// <summary>
    /// Método virtual CreateBehaviourTree, que crea el árbol de comportamiento
    /// </summary>
    public virtual void CreateBehaviourTree()
    {

    }

    /// <summary>
    /// Método ShowInformation, que muestra la información sobre los objetos
    /// </summary>
    public void ShowInformation()
    {
        if (!informationGameObject.gameObject.activeSelf)
        {
            informationGameObject.gameObject.SetActive(true);
            hasGivenInformation = true;
        }
    }

    /// <summary>
    /// Método HideInformation, que esconde la información sobre los objetos
    /// </summary>
    public void HideInformation()
    {
        if (informationGameObject.gameObject.activeSelf)
        {
            hasGivenInformation = false;
            informationGameObject.gameObject.SetActive(false);
            isWitness = false;
            isVictim = false;
        }
    }
    #endregion
}
