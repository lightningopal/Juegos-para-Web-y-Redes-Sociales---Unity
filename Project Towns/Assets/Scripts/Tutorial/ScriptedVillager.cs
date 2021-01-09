using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Clase ScriptedVillager, que controla a un aldeano scripteado para el tutorial
/// </summary>
public class ScriptedVillager : MonoBehaviour
{
    #region Variables
    [Header("Parámetros")]
    [Tooltip("ID del aldeano del tutorial")]
    public int tutorialID = 0;

    [Tooltip("Distancia para mostrar información al marshall si es víctima o testigo")]
    [SerializeField]
    protected float marshallInfoRange = 3.0f;

    [Header("Información sobre el NPC")]
    [Tooltip("Booleano que indica si es testigo")]
    [HideInInspector]
    public bool isWitness = false;
    [Tooltip("Booleano que indica si es víctima")]
    [HideInInspector]
    public bool isVictim = false;
    [Tooltip("Booleano que indica si ha dado información")]
    [HideInInspector]
    public bool hasGivenInformation = false;
    [Tooltip("Booleano que indica si ha sido llamado por el marshall")]
    [HideInInspector]
    public bool hasBeenCalledByMarshall = false;
    [Tooltip("Referencia al efecto de Víctima o de Testigo")]
    [HideInInspector]
    public GameObject infoVFX = null;

    [Header("Objetos")]
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
    [Tooltip("Items del NPC")]
    [HideInInspector]
    public NPCItems items;

    [Header("Otros")]
    [Tooltip("GameObject que contiene la información")]
    [SerializeField]
    public InformationObject informationGameObject = null;
    [Tooltip("Referencia al jugador")]
    [HideInInspector]
    public Transform playerTransform;
    [Tooltip("Agente NavMesh")]
    public NavMeshAgent thisAgent;
    [Tooltip("Animator")]
    [SerializeField]
    public Animator thisAnimator = null;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        // Referencia al jugador
        playerTransform = FindObjectOfType<TutorialPlayerController>().transform;

        // Hacer idle
        thisAnimator.SetTrigger("idle");
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        if (hasBeenCalledByMarshall)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), 10);
        }
        else
        {
            // Si está en rango para dar la info
            if (Vector3.Distance(this.transform.position, playerTransform.position) < marshallInfoRange)
            {
                // Si es el villager 1 oel 3, se acerca al player
                if (tutorialID == 1 || tutorialID == 3)
                {
                    if (Vector3.Distance(this.transform.position, playerTransform.position) < 5)
                    {
                        if (thisAgent.hasPath)
                        {
                            thisAgent.ResetPath();
                            thisAnimator.SetTrigger("idle");
                        }
                    }
                    else
                    {
                        if (!thisAgent.hasPath)
                        {
                            if (tutorialID == 1)
                            {
                                if (TutorialManager.instance.id1HasAlreadyGivenInfo)
                                    return;
                            }
                            else if (tutorialID == 3)
                            {
                                if (TutorialManager.instance.id3HasAlreadyGivenInfo)
                                    return;
                            }

                            thisAgent.SetDestination(playerTransform.position);
                            thisAgent.speed = 4;
                            thisAnimator.SetTrigger("run");
                        }

                    }
                }

                if (isVictim || isWitness)
                {
                    ShowInformation();
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), 10);

                    if (tutorialID == 0)
                    {
                        if (!TutorialManager.instance.activatedEvents[9])
                        {
                            TutorialManager.instance.activatedEvents[9] = true;
                            TutorialManager.instance.GoToNextStep();
                        }
                    }
                    else if (tutorialID == 1)
                    {
                        if (!TutorialManager.instance.activatedEvents[11])
                        {
                            TutorialManager.instance.id1HasAlreadyGivenInfo = true;
                            TutorialManager.instance.activatedEvents[11] = true;
                            TutorialManager.instance.GoToNextStep();
                        }
                    }
                    else if (tutorialID == 2)
                    {
                        if (!TutorialManager.instance.activatedEvents[14])
                        {
                            TutorialManager.instance.activatedEvents[14] = true;
                            TutorialManager.instance.GoToNextStep();
                        }
                    }
                    else if (tutorialID == 3)
                    {
                        if (!TutorialManager.instance.activatedEvents[16])
                        {
                            TutorialManager.instance.id3HasAlreadyGivenInfo = true;
                            TutorialManager.instance.activatedEvents[16] = true;
                            TutorialManager.instance.GoToNextStep();
                        }
                    }

                }
            }
            // Si no está en rango y ya la ha dado
            else if (hasGivenInformation)
            {
                HideInformation();
            }
            // Si no
            else
            {
                // Si es el villager 1 oel 3, se acerca al player
                if (tutorialID == 1 || tutorialID == 3)
                {
                    if (Vector3.Distance(this.transform.position, playerTransform.position) < 5)
                    {
                        if (thisAgent.hasPath)
                        {
                            thisAgent.ResetPath();
                            thisAnimator.SetTrigger("idle");
                        }
                    }
                    else
                    {
                        if (!thisAgent.hasPath)
                        {
                            if (tutorialID == 1)
                            {
                                if (TutorialManager.instance.id1HasAlreadyGivenInfo)
                                    return;
                            }
                            else if(tutorialID == 3)
                            {
                                if (TutorialManager.instance.id3HasAlreadyGivenInfo)
                                    return;
                            }

                            thisAgent.SetDestination(playerTransform.position);
                            thisAgent.speed = 4;
                            thisAnimator.SetTrigger("run");
                        }
                            
                    }
                }
            }
        }
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
        for (int i = 0; i < eyes.Length; i++)
        {
            eyes[i].SetActive(true);
        }

        switch (items.eyes.eyesNumber)
        {
            case 1:
                eyes[1].SetActive(false);
                eyes[2].SetActive(false);
                break;
            case 2:
                eyes[0].SetActive(false);
                break;
        }

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

    public void GetRobbed()
    {
        // Establecemos que es víctima
        isVictim = true;

        // Instanciamos el efecto que avisa al jugador
        if (infoVFX != null)
        {
            // Para que no se repitan
            Destroy(infoVFX);
        }
        infoVFX = Instantiate(TutorialGameManager.instance.victimVFX, transform);

        // Calculamos información
        informationGameObject.CalculateInformation();
    }

    /// <summary>
    /// Método SeeRobbery, que se llama cuando el aldeano ha visto un robo
    /// </summary>
    public void SeeRobbery()
    {
        // Establecemos que es testigo
        isWitness = true;
        informationGameObject.item2 = null;

        // Instanciamos el efecto que avisa al jugador
        if (infoVFX != null)
        {
            // Para que no se generen varios
            Destroy(infoVFX);
        }
        infoVFX = Instantiate(TutorialGameManager.instance.witnessVFX, transform);

        // Calculamos información
        informationGameObject.CalculateInformation();
    }

    /// <summary>
    /// Método ShowInformation, que muestra la información sobre los objetos
    /// </summary>
    public void ShowInformation()
    {
        if (!informationGameObject.gameObject.activeSelf)
        {
            // Se destruye el efecto que avisa al jugador
            if (infoVFX != null)
            {
                Destroy(infoVFX);
            }

            //Efecto de sonido
            AudioManager.instance.PlaySound("GiveInfo");

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