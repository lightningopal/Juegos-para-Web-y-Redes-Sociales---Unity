using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase GameManager, que controla la partida
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    public static GameManager instance;

    [Header("Parámetros de la partida")]
    [Tooltip("Índice de la dificultad elegida")]
    public int difficulty_index = 0;
    [Tooltip("Dificultad elegida")]
    [HideInInspector]
    public Difficulty difficulty = null;

    [Header("Zonas")]
    [Tooltip("Lista de zonas")]
    public List<Zone> zones = new List<Zone>();

    [Header("Valores de la partida")]
    [Tooltip("Contador de robos")]
    private int robberyCount = 0;
    [Tooltip("Contador de intentos")]
    private int attemptsCount = 0;

    // Flotantes para el tiempo que se ha tardado
    private float startGameTime, endGameTime;

    [Header("Parámetros generales")]
    [Tooltip("Partida en pausa")]
    [HideInInspector]
    public bool gamePaused = false;

    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Awake, that executes on script load
    /// </summary>
    void Awake()
    {
        // Se instancia a si misma
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        // Obtener la dificultad elegida
        string difficultyName = "";
        switch (difficulty_index)
        {
            case 0:
                difficultyName = "Easy";
                break;
            case 1:
                difficultyName = "Medium";
                break;
            case 2:
                difficultyName = "Hard";
                break;
        }

        difficulty = Resources.Load<Difficulty>("Difficulties/Difficulty_" + difficulty_index + "_" + difficultyName);
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {

    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método AddRobbery, que añade un robo al ladrón
    /// </summary>
    public void AddRobbery()
    {
        robberyCount++;
        if (robberyCount == difficulty.thiefRobberies)
            EndGame();
    }

    /// <summary>
    /// Método AddAttempt, que añade un intento fallido
    /// </summary>
    public void AddAttempt()
    {
        attemptsCount++;
        if (attemptsCount == difficulty.catchAttempts)
            EndGame();
    }

    /// <summary>
    /// Método PauseGame, que pausa la partida
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }

    /// <summary>
    /// Método ResumeGame, que reanuda la partida
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
    }

    /// <summary>
    /// Método EndGameAsWin, que acaba la partidacomo victoria
    /// </summary>
    public void EndGameAsWin()
    {
        // Acabar la partida
        EndGame();

        // Cosas de victoria
    }

    /// <summary>
    /// Método EndGameAsLose, que acaba la partida como derrota
    /// </summary>
    public void EndGameAsLose()
    {
        // Acabar la partida
        EndGame();

        // Cosas de derrota
    }

    /// <summary>
    /// Método EndGame, que acaba la partida
    /// </summary>
    public void EndGame()
    {
        // Se pausa la partida
        PauseGame();

        // Mostrar pantalla final
    }
    #endregion
}