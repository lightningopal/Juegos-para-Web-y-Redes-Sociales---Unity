using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/// <summary>
/// Clase TutorialPlayerController, que controla el movimiento del jugador en el tutorial
/// </summary>
public class TutorialPlayerController : MonoBehaviour
{
    #region Variables
    [Tooltip("Agente NavMesh")]
    [SerializeField]
    private NavMeshAgent thisAgent = null;
    [Tooltip("Destino")]
    private Vector3 destination = new Vector3();
    [Tooltip("Animator")]
    [SerializeField]
    private Animator thisAnimator = null;
    [Tooltip("LayerMask del propio Player")]
    [SerializeField]
    private LayerMask playerLayerMask = new LayerMask();

    [Tooltip("Efecto movimiento")]
    [SerializeField]
    private ParticleSystem effectPrefab = null;
    [Tooltip("Instancia del efecto")]
    private ParticleSystem effectInstance = null;

    [Header("Rangos")]
    [Tooltip("Rango para llegar al robo")]
    [SerializeField]
    private float QUIT_STEAL_ICON_RANGE = 10.0f;
    [Tooltip("Rango para detenerse cuando está cerca del NPC que ha llamado")]
    [SerializeField]
    private float DETENTION_RANGE = 2.0f;

    [Header("Físicas NavMesh")]
    [Tooltip("Velocidad de Marshallow")]
    [SerializeField]
    private float navMesh_speed = 8.0f;
    [Tooltip("Velocidad angular")]
    [SerializeField]
    private float navMesh_angularSpeed = 5000.0f;
    [Tooltip("Aceleración")]
    [SerializeField]
    private float navMesh_acceleration = 12.0f;
    [Tooltip("Distancia para detenerse")]
    [SerializeField]
    private float navMesh_stoppingDistance = 0.2f;
    [Tooltip("Auto Freno (?)")]
    [SerializeField]
    private bool navMesh_autoBraking = true;

    [Header("Detención")]
    [Tooltip("NPC en detención")]
    private ScriptedVillager calledScriptedVillager = null;
    [Tooltip("Booleano que indica si el NPC a detener es el ladrón")]
    private bool calledScriptedVillagerIsThief = false;

    [Header("Tutorial")]
    [SerializeField]
    private TutorialManager tutorialManager = null;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        // Valores NavMesh
        thisAgent.speed = navMesh_speed;
        thisAgent.angularSpeed = navMesh_angularSpeed;
        thisAgent.acceleration = navMesh_acceleration;
        thisAgent.stoppingDistance = navMesh_stoppingDistance;
        thisAgent.autoBraking = navMesh_autoBraking;
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        // Si el jugador tiene que esperar, no realizamos cálculos
        if (tutorialManager.playerHasToWait)
            return;

        // Si el jugador hace click con el ratón
        if (Input.GetMouseButtonDown(0))
        {
            // Si puede moverse
            if (tutorialManager.playerCanMove &&
                !tutorialManager.event22MustArrestHornsVillager &&
                !tutorialManager.event26MustArrestThief)
            {
                // Si el juego está en pausa, no realizamos cálculos
                if (TutorialGameManager.instance.gamePaused)
                    return;

                // Si la partida ha acabado, no realizamos cálculos
                if (TutorialGameManager.instance.gameOver)
                    return;

                // Si está el ratón sobre la UI, no se lanza Raycast
                if (IsPointerOverUIObject())
                    return;

                // En caso contrario, se lanza el Raycast
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f, ~playerLayerMask))
                {
                    // Va hacia allí
                    destination = hit.point;
                    thisAgent.SetDestination(destination);
                    if (!thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("Marshallow_Run"))
                        thisAnimator.SetTrigger("run");

                    // Si es un aldeano
                    if (hit.transform.CompareTag("ScriptedVillager"))
                    {
                        // Eventos tutorial
                        if (!tutorialManager.event25MustClickThief &&
                            !tutorialManager.event26MustArrestThief)
                        {
                            if (tutorialManager.actualEvent == 20 &&
                                !tutorialManager.activatedEvents[20])
                            {
                                // Si ya tenia uno, lo quitamos
                                if (calledScriptedVillager != null)
                                {
                                    calledScriptedVillager.hasBeenCalledByMarshall = false;
                                    calledScriptedVillager = null;
                                }

                                calledScriptedVillagerIsThief = false;
                                calledScriptedVillager = hit.transform.gameObject.GetComponent<ScriptedVillager>();
                                calledScriptedVillager.hasBeenCalledByMarshall = true;

                                // Si es el aldeano 4 y estamos en el evento adecuado
                                if (calledScriptedVillager.tutorialID == 4)
                                {
                                    // Activamos el evento y llamamos al siguiente
                                    tutorialManager.activatedEvents[20] = true;
                                    tutorialManager.GoToNextStep();

                                    TutorialGameManager.instance.ShowDetentionButton(calledScriptedVillager.transform);

                                    // Efecto de sorpresa
                                    GameObject surpriseVFX = Instantiate(TutorialGameManager.instance.surpriseVFX, calledScriptedVillager.transform);
                                    ParticleSystem partS = surpriseVFX.GetComponent<ParticleSystem>();
                                    float totalDuration = partS.main.duration + partS.main.startLifetime.constant;
                                    Destroy(surpriseVFX, totalDuration);
                                }
                            }
                        }
                    }
                    // Si es el ladrón
                    else if (hit.transform.CompareTag("ScriptedThief"))
                    {
                        // Eventos tutorial
                        if (!tutorialManager.event20MustClickHornsVillager &&
                            !tutorialManager.event22MustArrestHornsVillager)
                        {
                            // Si ya tenia uno, lo quitamos
                            if (calledScriptedVillager != null)
                            {
                                calledScriptedVillager.hasBeenCalledByMarshall = false;
                                calledScriptedVillager = null;
                            }

                            calledScriptedVillagerIsThief = true;
                            calledScriptedVillager = hit.transform.gameObject.GetComponent<ScriptedVillager>();
                            calledScriptedVillager.hasBeenCalledByMarshall = true;

                            // Si es el aldeano 4 y estamos en el evento adecuado
                            if (tutorialManager.actualEvent == 25 &&
                                !tutorialManager.activatedEvents[25])
                            {
                                // Activamos el evento y llamamos al siguiente
                                tutorialManager.activatedEvents[25] = true;
                                tutorialManager.GoToNextStep();

                                TutorialGameManager.instance.ShowDetentionButton(calledScriptedVillager.transform);

                                // Efecto de sorpresa
                                GameObject surpriseVFX = Instantiate(TutorialGameManager.instance.surpriseVFX, calledScriptedVillager.transform);
                                ParticleSystem partS = surpriseVFX.GetComponent<ParticleSystem>();
                                float totalDuration = partS.main.duration + partS.main.startLifetime.constant;
                                Destroy(surpriseVFX, totalDuration);
                            }
                        }
                    }
                    // Si es territorio transitable, mueve al agente a esa posición
                    else if (hit.transform.CompareTag("Walkable") || hit.transform.CompareTag("Zone"))
                    {
                        /// Tutorial
                        /// Evento 1
                        // Si no ha completado el evento 1, se añade un paso
                        if (!tutorialManager.activatedEvents[1])
                        {
                            tutorialManager.event1Moves++;
                            // Si ha dado tres pasos, se completa el evento 1
                            if (tutorialManager.event1Moves >= 3)
                            {
                                tutorialManager.activatedEvents[1] = true;
                                tutorialManager.GoToNextStep();
                            }
                        }

                        // Si había llamado a un NPC, lo deja ir
                        if (calledScriptedVillager != null &&
                            !tutorialManager.event20MustClickHornsVillager &&
                            !tutorialManager.event22MustArrestHornsVillager &&
                            !tutorialManager.event25MustClickThief &&
                            !tutorialManager.event26MustArrestThief)
                        {
                            calledScriptedVillager.hasBeenCalledByMarshall = false;
                            calledScriptedVillager = null;
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
            // Si no puede moverse y no tiene que arrestar
            else if (!tutorialManager.event22MustArrestHornsVillager &&
                !tutorialManager.event26MustArrestThief)
            {
                // Si no puede usar la UI
                if (!tutorialManager.playerCanUseUI && !tutorialManager.event29CanReturnToMenu)
                    tutorialManager.GoToNextStep();
            }
        }

        // Si llega al destino, cambia de animación
        if (Vector3.Distance(destination, transform.position) <= 1.0f)
        {
            if (!thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("Marshallow_Idle"))
                thisAnimator.SetTrigger("idle");
        }


        // Si está lo suficientemente cerca del NPC que ha llamado, se para
        if (calledScriptedVillager != null)
        {
            if (Vector3.Distance(this.transform.position, calledScriptedVillager.transform.position) < DETENTION_RANGE)
            {
                if (thisAgent.hasPath)
                {
                    thisAgent.ResetPath();
                    thisAnimator.SetTrigger("idle");
                }
            }
        }

        // Si está lo suficientemente cerca de un robo, lo desactiva
        List<Robbery> closeRobberies = new List<Robbery>();
        foreach (Robbery r in TutorialGameManager.instance.robberies)
        {
            if (Vector3.Distance(this.transform.position, r.robberyPosition) < QUIT_STEAL_ICON_RANGE)
            {
                closeRobberies.Add(r);
            }
        }

        foreach (Robbery r in closeRobberies)
        {
            TutorialUIManager.instance.HideRobberyIcon(r);
        }

    }

    /// <summary>
    /// Método OnTriggerEnter, que comprueba si el jugador ha entrado en un trigger
    /// </summary>
    /// <param name="other">Trigger en el que entra</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Forge"))
        {
            if (tutorialManager.actualEvent == 6 &&
                !tutorialManager.activatedEvents[6])
            {
                tutorialManager.activatedEvents[6] = true;
                tutorialManager.GoToNextStep();
            }
        }

        if (other.CompareTag("Square"))
        {
            if (tutorialManager.actualEvent == 18 &&
                !tutorialManager.activatedEvents[18])
            {
                tutorialManager.activatedEvents[18] = true;
                tutorialManager.GoToNextStep();
            }
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
        if (calledScriptedVillagerIsThief)
        {
            // Tutorial
            if (tutorialManager.actualEvent == 26 &&
                !tutorialManager.activatedEvents[26])
            {
                tutorialManager.activatedEvents[26] = true;
                tutorialManager.GoToNextStep();
            }

            // Efecto
            GameObject nervousVFX = Instantiate(TutorialGameManager.instance.nervousVFX, calledScriptedVillager.transform);
            // Efecto de sonido
            AudioManager.instance.PlaySound("Nervous");
            
            TutorialGameManager.instance.HideDetentionButton();
            TutorialGameManager.instance.EndGameAsWin();
        }
        // Si falló
        else
        {
            // Tutorial
            if (tutorialManager.actualEvent == 22 &&
                !tutorialManager.activatedEvents[22])
            {
                tutorialManager.activatedEvents[22] = true;
                tutorialManager.GoToNextStep();
            }

            // Efecto
            GameObject angerVFX = Instantiate(TutorialGameManager.instance.angerVFX, calledScriptedVillager.transform);
            // Se destruye cuando acaba el efecto
            ParticleSystem partS = angerVFX.GetComponent<ParticleSystem>();
            float totalDuration = partS.main.duration + partS.main.startLifetime.constant;
            Destroy(angerVFX, totalDuration);

            calledScriptedVillager.hasBeenCalledByMarshall = false;
            calledScriptedVillager = null;
            TutorialGameManager.instance.HideDetentionButton();
            TutorialGameManager.instance.AddAttempt();
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
