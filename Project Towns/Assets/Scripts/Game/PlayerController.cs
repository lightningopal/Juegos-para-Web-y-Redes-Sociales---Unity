using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/// <summary>
/// Clase PlayerController, que controla el movimiento del jugador
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Variables
    [Tooltip("Agente NavMesh")]
    [SerializeField]
    private NavMeshAgent thisAgent = null;
    [Tooltip("LayerMask del propio Player")]
    [SerializeField]
    private LayerMask playerLayerMask = new LayerMask();

    [Tooltip("Efecto movimiento")]
    [SerializeField]
    private ParticleSystem effectPrefab = null;
    private ParticleSystem effectInstance = null;

    [Header("Rangos")]
    [Tooltip("Rango para llegar al robo")]
    [SerializeField]
    private float QUIT_STEAL_ICON_RANGE = 10.0f;
    [Tooltip("Rango para detenerse cuando está cerca del NPC")]
    [SerializeField]
    private float DETENTION_RANGE = 2.0f;

    [Header("Detención")]
    [Tooltip("NPC en detención")]
    private NPC calledNPC = null;
    [Tooltip("Booleano que indica si el NPC a detener es el ladrón")]
    private bool calledNPCIsThief = false;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        // Si el jugador hace click con el ratón
        if (Input.GetMouseButtonDown(0))
        {
            // Si está el ratón sobre la UI, no se lanza Raycast
            if (IsPointerOverUIObject())
                return;

            // En caso contrario, se lanza el Raycast
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, ~playerLayerMask)) 
            {
                // Comprueba con qué ha chocado el raycast
                //Debug.Log("You selected the: " + hit.transform.name);

                // Va hacia allí
                thisAgent.SetDestination(hit.point);

                // Si es un aldeano
                if (hit.transform.CompareTag("Villager"))
                {
                    // Si ya tenia uno, lo quitamos
                    if (calledNPC != null)
                    {
                        calledNPC.hasBeenCalledByMarshall = false;
                        calledNPC = null;
                    }

                    calledNPCIsThief = false;
                    calledNPC = hit.transform.gameObject.GetComponent<Villager>();
                    calledNPC.hasBeenCalledByMarshall = true;
                    GameManager.instance.ShowDetentionButton(calledNPC.transform);
                }
                // Si es el ladrón
                else if (hit.transform.CompareTag("Thief"))
                {
                    // Si ya tenia uno, lo quitamos
                    if (calledNPC != null)
                    {
                        calledNPC.hasBeenCalledByMarshall = false;
                        calledNPC = null;
                    }

                    calledNPCIsThief = true;
                    calledNPC = hit.transform.gameObject.GetComponent<Thief>();
                    calledNPC.hasBeenCalledByMarshall = true;
                    GameManager.instance.ShowDetentionButton(calledNPC.transform);
                }
                // Si es territorio transitable, mueve al agente a esa posición
                else if(hit.transform.CompareTag("Walkable") || hit.transform.CompareTag("Zone"))
                {
                    // Si había llamado a un NPC, lo deja ir
                    if (calledNPC != null)
                    {
                        calledNPC.hasBeenCalledByMarshall = false;
                        calledNPC = null;
                        GameManager.instance.HideDetentionButton();
                    }
                    
                    // Se instancia el efecto
                    if (effectInstance == null)
                    {
                        effectInstance = Instantiate(effectPrefab, hit.point + new Vector3(0, 0.1f, 0), new Quaternion());
                    }
                    else
                    {
                        effectInstance.Clear();
                        effectInstance.gameObject.transform.position = hit.point + new Vector3(0, 0.1f, 0);
                        effectInstance.Play();
                    }
                }
            }
        }

        // Si está lo suficientemente cerca del NPC que ha llamado, se para
        if (calledNPC != null)
        {
            if (Vector3.Distance(this.transform.position, calledNPC.transform.position) < DETENTION_RANGE)
            {
                if (thisAgent.hasPath)
                    thisAgent.ResetPath();
            }
        }

        // Si está lo suficientemente cerca de un robo, lo desactiva
        List<Robbery> closeRobberies = new List<Robbery>();
        foreach (Robbery r in GameManager.instance.robberies)
        {
            if (Vector3.Distance(this.transform.position, r.robberyPosition) < QUIT_STEAL_ICON_RANGE)
            {
                closeRobberies.Add(r);
            }
        }

        foreach(Robbery r in closeRobberies)
        {
            UIManager.instance.HideRobberyIcon(r);
        }

        
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método Detention, que ejecuta una detención
    /// </summary>
    public void Detention()
    {
        // Si acertó
        if (calledNPCIsThief)
        {
            // FALTA: Mostrar icono de sudor al Thief
            GameManager.instance.HideDetentionButton();
            GameManager.instance.EndGameAsWin();
        }
        // Si falló
        else
        {
            // FALTA: Mostrar icono de enfado al Villager
            calledNPC.hasBeenCalledByMarshall = false;
            calledNPC = null;
            GameManager.instance.HideDetentionButton();
            GameManager.instance.AddAttempt();
        }
    }

    /// <summary>
    /// Método IsPointerOverUIObject, que comprueba si el ratón está sobre un elemento de la UI
    /// </summary>
    /// <returns>Booleano que indica si el ratón está sobre la UI</returns>
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    #endregion
}
