using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/// <summary>
/// Clase PlayerController, que controla el movimiento del jugador
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Tooltip("Agente NavMesh")]
    [SerializeField]
    private NavMeshAgent thisAgent = null;

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
                if (hit.transform.CompareTag("Walkable"))
                {
                    thisAgent.SetDestination(hit.point);
                }
            }
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
}
