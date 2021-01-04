﻿using System.Collections.Generic;
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

    [Tooltip("Efecto movimiento")]
    [SerializeField]
    private ParticleSystem effectPrefab = null;
    private ParticleSystem effectInstance = null;

    [Tooltip("Rango para llegar al robo")]
    [SerializeField]
    private float QUIT_STEAL_ICON_RANGE = 1.0f;
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
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                // Comprueba con qué ha chocado el raycast
                //Debug.Log("You selected the: " + hit.transform.name);

                // Si es territorio transitable, mueve al agente a esa posición
                if (hit.transform.CompareTag("Walkable") || hit.transform.CompareTag("Zone"))
                {
                    thisAgent.SetDestination(hit.point);
                    // Se instancia el efecto
                    if (effectInstance == null)
                    {
                        effectInstance = Instantiate(effectPrefab, hit.point + new Vector3(0,0.1f,0), new Quaternion());
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
