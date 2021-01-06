using UnityEngine;

/// <summary>
/// Clase Difficulty, objeto scriptable para crear niveles de dificultad
/// </summary>
[CreateAssetMenu]
public class Difficulty : ScriptableObject
{
    #region Variables
    [Header("Parámetros de la dificultad")]
    [Tooltip("Índice de la dificultad")]
    public int difficultyIndex = 0;
    [Tooltip("Número de aldeanos")]
    public int villagers = 40;
    [Tooltip("Intentos de cazada")]
    public int catchAttempts = 3;
    [Tooltip("Máximo robos del ladrón")]
    public int thiefRobberies = 5;
    /*[Tooltip("Tipos de prenda")]
    [HideInInspector]
    public int clothings = 8;*/

    [Header("Aldeano")]
    [Tooltip("Probabilidad de que ambos datos sean seguros siendo víctima (2 verdad SIN ?)")]
    [Range(0.0f, 100.0f)]
    public float victimSafeProbability = 50.0f;
    [Tooltip("Probabilidad de que ambos datos sean veraces siendo víctima (2 verdad CON ?)")]
    [Range(0.0f, 100.0f)]
    public float victimVeracityProbability = 50.0f;
    [Tooltip("Probabilidad de que el dato sea seguro siendo testigo (1 verdad SIN ?)")]
    [Range(0.0f, 100.0f)]
    public float witnessSafeProbability = 50.0f;
    [Tooltip("Probabilidad de que el dato sea veraz siendo testigo (1 verdad CON ?)")]
    [Range(0.0f, 100.0f)]
    public float witnessVeracityProbability = 50.0f;
    [Tooltip("Probabilidad DE LOS ALDEANOS de correr hacia su destino")]
    [Range(0.0f, 100.0f)]
    public float VILLAGER_SPEED_RUN_PROBABILITY = 30.0f;


    [Header("Ladrón")]
    [Tooltip("Tiempo entre robos")]
    public float timeBetweenSteals = 30.0f;
    [Tooltip("Probabilidad DEL LADRÓN de correr hacia su destino")]
    [Range(0.0f, 100.0f)]
    public float THIEF_SPEED_RUN_PROBABILITY = 100.0f;
    #endregion
}