using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Clase PlayerController, que controla el movimiento del jugador
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Tooltip("Agente NavMesh")]
    public NavMeshAgent thisAgent;

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        // Si el jugador hace click con el ratón, lanza un raycast
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                // Comprueba con qué ha chocado el raycast
                Debug.Log("You selected the: " + hit.transform.name);

                // Si es territorio transitable, mueve al agente a esa posición
                if (hit.transform.CompareTag("Walkable"))
                {
                    thisAgent.SetDestination(hit.point);
                }
            }
        }
    }
}
