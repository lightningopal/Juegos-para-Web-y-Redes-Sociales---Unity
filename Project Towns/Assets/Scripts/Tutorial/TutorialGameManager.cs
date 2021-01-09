using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase TutorialGameManager, que controla la partida del tutorial
/// </summary>
public class TutorialGameManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    public static TutorialGameManager instance;

    [Tooltip("Dificultad del tutorial - Fácil")]
    private Difficulty easyDifficulty;

    [Header("Aldeanos")]
    [Tooltip("Lista de aldeanos")]
    public List<ScriptedVillager> villagers = new List<ScriptedVillager>();

    [Header("VFX")]
    public GameObject victimVFX = null;
    public GameObject witnessVFX = null;
    public GameObject angerVFX = null;
    public GameObject nervousVFX = null;
    public GameObject surpriseVFX = null;
    public GameObject victoryVFX = null;
    public GameObject defeatVFX = null;

    [Header("Valores de la partida")]
    [Tooltip("Contador de robos")]
    private int thiefRobberies;
    [Tooltip("Contador de intentos")]
    private int attemptsCount;

    [Header("Otros")]
    [Tooltip("Robos")]
    public List<Robbery> robberies = new List<Robbery>();
    [Tooltip("Partida en pausa")]
    [HideInInspector]
    public bool gamePaused = false;
    [Tooltip("Partida finalizada")]
    [HideInInspector]
    public bool gameOver = false;
    [Tooltip("Tiempo entre acabar y la pantalla final")]
    [SerializeField]
    private float endTimeWait = 2.0f;

    [Header("Otros")]
    [Tooltip("Level Loader")]
    [SerializeField]
    private LevelLoader levelLoader = null;
    [Tooltip("Botón de detención")]
    [SerializeField]
    private CustomDetentionButton detentionButton = null;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Awake, que se ejecuta cuando carga el script
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
        // Dificultad fácil
        easyDifficulty = Resources.Load<Difficulty>("Difficulties/Difficulty_0_Easy");

        // Asignar valores de los robos
        thiefRobberies = 0;
        attemptsCount = easyDifficulty.catchAttempts;

        // Actualizar UI
        // Textos
        TutorialUIManager.instance.UpdateRobberiesText(thiefRobberies, easyDifficulty.thiefRobberies);
        TutorialManager.instance.UpdateTutorialTranslate();

        // Despausa la partida (si estuviera en pausa)
        ResumeGame();

        // Establecemos el gameOver a false
        gameOver = false;
    }
    #endregion

    #region MétodosClase

    /// <summary>
    /// Método AddRobbery, que añade un robo al ladrón
    /// </summary>
    public void AddRobbery()
    {
        thiefRobberies++;
        TutorialUIManager.instance.UpdateRobberiesText(thiefRobberies, easyDifficulty.thiefRobberies);
    }

    /// <summary>
    /// Método AddAttempt, que resta un intento fallido
    /// </summary>
    public void AddAttempt()
    {
        attemptsCount--;
        TutorialUIManager.instance.UpdateAttempts(attemptsCount);
    }

    public void ShowDetentionButton(Transform targetTransform_)
    {
        detentionButton.targetTransform = targetTransform_;
        detentionButton.gameObject.SetActive(true);
        detentionButton.CalculatePosition();
    }

    public void HideDetentionButton()
    {
        detentionButton.targetTransform = null;
        detentionButton.gameObject.SetActive(false);
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
        // Actualizar UI por victoria
        TutorialUIManager.instance.UpdateEndGameScreen();

        // Establecemos el gameOver a true
        gameOver = true;

        // Acabar la partida
        Invoke("EndGame", endTimeWait);

        // Sonido y música de perder
        Invoke("PlayWinSound", endTimeWait + 0.5f);
    }

    /// <summary>
    /// Método EndGame, que acaba la partida
    /// </summary>
    public void EndGame()
    {
        // Se pausa la partida
        //PauseGame();
        gamePaused = true;

        // Mostrar pantalla final
        levelLoader.LoadCanvas(3);
        levelLoader.UseCircle(true);
        levelLoader.ShowBars(true);
    }

    /// <summary>
    /// Suena el efecto de sonido de victoria
    /// </summary>
    public void PlayWinSound()
    {
        AudioManager.instance.ManageAudio("WinSound", "sound", "play");
        Invoke("PlayWinMusic", 3);
    }

    /// <summary>
    /// Suena la música de victoria
    /// </summary>
    public void PlayWinMusic()
    {
        AudioManager.instance.ManageAudio("WinMusic", "music", "play");
        Time.timeScale = 0;
    }

    /// <summary>
    /// Método UpdateRobberiesTranslate, que actualiza traducido el texto de los robos
    /// </summary>
    public void UpdateRobberiesTranslate()
    {
        if (TutorialUIManager.instance != null && easyDifficulty != null)
            TutorialUIManager.instance.UpdateRobberiesText(thiefRobberies, easyDifficulty.thiefRobberies);
    }
    #endregion
}