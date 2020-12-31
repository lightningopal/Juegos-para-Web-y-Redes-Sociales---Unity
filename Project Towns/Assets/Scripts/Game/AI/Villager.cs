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

    [Header("Información sobre el aldeano")]
    [Tooltip("Booleano que indica si es víctima")]
    [HideInInspector]
    public bool isVictim = false;
    [Tooltip("Booleano que indica si es testigo")]
    [HideInInspector]
    public bool isWitness = false;

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

    // Referencia al ladrón
    private Transform thief;

    [Header("Otros")]
    [Tooltip("GameObject que contiene la información")]
    [SerializeField]
    private GameObject informationGameObject = null;
    [Tooltip("Agente NavMesh")]
    [SerializeField]
    private NavMeshAgent _thisAgent = null;
    public NavMeshAgent thisAgent
    {
        get { return _thisAgent; }
    }

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
        thief = FindObjectOfType<PlayerController>().transform;

        // Crear Árbol
        //CreateBehaviourTree();
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        /*if (HasSeenRobbery())
        {
            thisAgent.SetDestination(thief.position);
        }*/
    }

    #region Colliders
    /// <summary>
    /// Método OnTriggerEnter, que se llama al entrar en un trigger
    /// </summary>
    /// <param name="other">Trigger en el que entra</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("thief"))
        {
            if (isWitness || isVictim)
            {
                ShowInformation();
            }
        }
    }

    /// <summary>
    /// Método OnTriggerExit, que se llama al sair de un trigger
    /// </summary>
    /// <param name="other">Trigger del que sale</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("thief"))
        {
            if (isWitness || isVictim)
            {
                isWitness = false;
                isVictim = false;

                HideInformation();
            }
        }
    }
    #endregion

    /// <summary>
    /// Método OnDrawGizmosSelected, que dibuja gizmos en la escena al seleccionar el personaje
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionDistance);

        Gizmos.color = Color.gray;

        if (thief != null)
            Gizmos.DrawLine(transform.position, transform.position +
                (thief.position - transform.position).normalized * visionDistance);

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
        /*VictimNode newVictimNode = new VictimNode(this);

        topNode = new Selector(new List<Node>() { newSelector });*/
    }

    /// <summary>
    /// Método HasSeenRobbery, para comprobar si ha visto el robo
    /// </summary>
    /// <returns>Booleano que indica si ha visto el robo</returns>
    private bool HasSeenRobbery()
    {
        // Si el ladrón no está en el ángulo, no lo ve
        if (Vector3.Angle(transform.forward.normalized,
            (thief.position - transform.position).normalized) > visionAngle / 2)
        {
            return false;
        }  

        // Lanzamos rayos para saber si lo ve
        RaycastHit[] hits = Physics.RaycastAll(transform.position,
            (thief.position - transform.position).normalized, visionDistance);

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
    private void ShowInformation()
    {
        if (!informationGameObject.activeSelf)
            informationGameObject.SetActive(true);
    }

    /// <summary>
    /// Método HideInformation, que esconde la información sobre los objetos
    /// </summary>
    private void HideInformation()
    {
        if (informationGameObject.activeSelf)
            informationGameObject.SetActive(false);
    }
    #endregion
}
