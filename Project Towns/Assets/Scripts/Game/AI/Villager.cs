using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase Villager, que controla a los aldeanos de la partida
/// </summary>
public class Villager : MonoBehaviour
{
    #region Variables
    [Header("Parámetros")]
    [Tooltip("Probabilidad de que ambos datos sean seguros siendo víctima")]
    public int victimSafeProbability = 50;
    [Tooltip("Probabilidad de que ambos datos sean veraces siendo víctima")]
    public int victimVeracityProbability = 50;
    [Tooltip("Probabilidad de que el dato sea seguro siendo testigo")]
    public int witnessSafeProbability = 50;
    [Tooltip("Probabilidad de que el dato sea veraz siendo testigo")]
    public int witnessVeracityProbability = 50;

    [Header("Información sobre el aldeano")]
    [Tooltip("Booleano que indica si es víctima")]
    public bool isVictim = false;
    [Tooltip("Booleano que indica si es testigo")]
    public bool isWitness = false;
    

    // GameObject que contiene la información
    [SerializeField]
    private GameObject informationGameObject;

    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        
    }

    #region Colliders
    /// <summary>
    /// Método OnTriggerEnter, que se llama al entrar en un trigger
    /// </summary>
    /// <param name="other">Trigger en el que entra</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isWitness || isVictim)
            {
                //ShowInformation();
            }
        }
    }

    /// <summary>
    /// Método OnTriggerExit, que se llama al sair de un trigger
    /// </summary>
    /// <param name="other">Trigger del que sale</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isWitness || isVictim)
            {
                isWitness = false;
                isVictim = false;

                //HideInformation();
            }
        }
    }
    #endregion
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método GetRobbed, para cuando roban al aldeano
    /// </summary>
    private void GetRobbed()
    {
        // Generamos un número aleatorio
        int randomNumber = Random.Range(0, 100);

        // Sigo mañana
    }
    #endregion
}
