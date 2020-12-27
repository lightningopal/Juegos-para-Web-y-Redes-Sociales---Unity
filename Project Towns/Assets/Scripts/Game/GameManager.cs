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
    [SerializeField]
    private List<Zone> zones = new List<Zone>();

    [Header("Aldeanos")]
    [Tooltip("Objeto padre de los aldeanos")]
    [SerializeField]
    private GameObject villagersParent = null;
    [Tooltip("Prefab del aldeano")]
    [SerializeField]
    private GameObject villagerPrefab = null;
    [Tooltip("Lista de putos de spawn aldeanos")]
    [SerializeField]
    private List<Transform> villagerPoints = new List<Transform>();
    [Tooltip("Lista de aldeanos")]
    private List<Villager> villagers = new List<Villager>();

    [Header("Valores de la partida")]
    [Tooltip("Contador de robos")]
    private int robberiesLeft;
    [Tooltip("Contador de intentos")]
    private int attemptsCount;

    // Flotantes para el tiempo que se ha tardado
    private float startGameTime, endGameTime;

    [Header("Parámetros generales")]
    [Tooltip("Partida en pausa")]
    [HideInInspector]
    public bool gamePaused = false;

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
        // Obtener la dificultad elegida
        string difficultyName = "";
        //difficulty_index = GlobalVars.instance.difficulty;
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

        // Asignar valores de la dificultad
        robberiesLeft = difficulty.thiefRobberies;
        attemptsCount = difficulty.catchAttempts;

        // Actualizar UI
        // Textos
        UIManager.instance.UpdateRobberiesText(robberiesLeft, difficulty.thiefRobberies);

        // Intentos
        //UIManager.instance.InitializeAttempts(difficulty.catchAttempts);

        // Spawnear Aldeanos
        //SpawnVillagers();

        // Despausa la partida (si estuviera en pausa)
        ResumeGame();
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {

    }
    #endregion

    #region MétodosClase
    public void SpawnVillagers()
    {
        List<Transform> updatedVillagerPoints = new List<Transform>(villagerPoints);
        for (int i = 0; i < difficulty.villagers; i++)
        {
            // Obtener datos aleatorios
            int randomNumber = Random.Range(0, updatedVillagerPoints.Count);
            Transform randomPoint = updatedVillagerPoints[randomNumber];
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            // Instanciar objeto
            GameObject villagerGameObject = Instantiate(villagerPrefab, randomPoint.position, randomRotation, villagersParent.transform);

            // Obtener componente Villager
            Villager newVillager = villagerGameObject.GetComponent<Villager>();

            // Comprobamos si ya existe uno igual
            while (CheckDuplicateVillager(newVillager))
            {
                newVillager.RandomizeVillager();
            }

            // Añadimos al aldeano a la lista
            villagers.Add(newVillager);

            // Borrar posición de la lista
            updatedVillagerPoints.Remove(randomPoint);
        }
    }

    /// <summary>
    /// Método CheckDuplicateVillager, que comprueba si ya hay un aldeano exactamente igual
    /// </summary>
    public bool CheckDuplicateVillager(Villager thisVillager)
    {
        VillagerItems thisVillagerItems = thisVillager.items;
        VillagerItems villagerInListItems;
        foreach (Villager villagerInList in villagers)
        {
            villagerInListItems = villagerInList.items;
            if (thisVillagerItems.villagerColor.itemName == villagerInListItems.villagerColor.itemName &&
                thisVillagerItems.eyesNumber == villagerInListItems.eyesNumber &&
                thisVillagerItems.hatItem.itemName == villagerInListItems.hatItem.itemName &&
                thisVillagerItems.hornItem.itemName == villagerInListItems.hornItem.itemName &&
                thisVillagerItems.neckItem.itemName == villagerInListItems.neckItem.itemName)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Método AddRobbery, que resta un robo al ladrón
    /// </summary>
    public void AddRobbery()
    {
        robberiesLeft--;
        UIManager.instance.UpdateRobberiesText(robberiesLeft, difficulty.thiefRobberies);
        if (robberiesLeft == 0)
            EndGame();
    }

    /// <summary>
    /// Método AddAttempt, que resta un intento fallido
    /// </summary>
    public void AddAttempt()
    {
        attemptsCount--;
        UIManager.instance.UpdateAttempts(attemptsCount);
        if (attemptsCount == 0)
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