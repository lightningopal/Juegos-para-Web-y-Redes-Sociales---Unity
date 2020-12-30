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
    [Tooltip("Tipos de prenda")]
    public int clothings = 8;
    #endregion
}