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

    [Header("Zonas")]
    [Tooltip("Lista de zonas")]
    [SerializeField]
    public List<Zone> zones = new List<Zone>();

    [Tooltip("Dificultad del tutorial - Fácil")]
    private Difficulty easyDifficulty;

    [Header("Aldeanos")]
    [Tooltip("Objeto padre de los aldeanos")]
    [SerializeField]
    private GameObject villagersParent = null;
    [Tooltip("Prefab del aldeano")]
    [SerializeField]
    private GameObject villagerPrefab = null;
    [Tooltip("Prefab del ladrón")]
    [SerializeField]
    private GameObject thiefPrefab = null;
    [Tooltip("Lista de putos de spawn aldeanos")]
    [SerializeField]
    private List<Transform> villagerPoints = new List<Transform>();
    [Tooltip("Lista de aldeanos")]
    public List<Villager> villagers = new List<Villager>();
    [Tooltip("Ladrón")]
    private Thief thief = null;

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
    [Tooltip("Robos")]
    public List<Robbery> robberies = new List<Robbery>();

    [Header("Parámetros generales")]
    [Tooltip("Partida en pausa")]
    [HideInInspector]
    public bool gamePaused = false;
    [Tooltip("Level Loader")]
    [SerializeField]
    private LevelLoader levelLoader = null;
    [Tooltip("Botón de detención")]
    [SerializeField]
    private CustomDetentionButton detentionButton = null;
    [Tooltip("Tiempo entre acabar y la pantalla final")]
    [SerializeField]
    private float endTimeWait = 2.0f;

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
        UIManager.instance.UpdateRobberiesText(thiefRobberies, easyDifficulty.thiefRobberies);

        // Intentos
        UIManager.instance.InitializeAttempts(easyDifficulty);

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

        /// Generar Ladrón
        // Obtener datos aleatorios
        int randomNumber = Random.Range(0, updatedVillagerPoints.Count);
        Transform randomPoint = updatedVillagerPoints[randomNumber];
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        // Instanciar objeto
        GameObject thiefGameObject = Instantiate(thiefPrefab, randomPoint.position, randomRotation, villagersParent.transform);

        // Obtener componente Villager
        Thief newThief = thiefGameObject.GetComponent<Thief>();

        // Se aleatoriza
        newThief.RandomizeNPC();

        // Instanciamos sus objetos
        newThief.PutItems();

        // Asignar a la referencia
        thief = newThief;

        // Borrar posición de la lista
        updatedVillagerPoints.Remove(randomPoint);

        /// Generar aldeanos
        for (int i = 0; i < (easyDifficulty.villagers - 1); i++)
        {
            // Obtener datos aleatorios
            randomNumber = Random.Range(0, updatedVillagerPoints.Count);
            randomPoint = updatedVillagerPoints[randomNumber];
            randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            // Instanciar objeto
            GameObject villagerGameObject = Instantiate(villagerPrefab, randomPoint.position, randomRotation, villagersParent.transform);

            // Obtener componente Villager
            Villager newVillager = villagerGameObject.GetComponent<Villager>();

            // Aleatorizamos el aldeano y comprobamos si ya existe uno igual
            do
            {
                newVillager.RandomizeNPC();
            } while (CheckDuplicateVillager(newVillager));

            // Instanciamos sus objetos
            newVillager.PutItems();

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
        NPCItems thisVillagerItems = thisVillager.items;
        NPCItems villagerInListItems;

        // Comprobar si es igual que el ladrón
        villagerInListItems = thief.items;
        if (thisVillagerItems.ToString().Equals(villagerInListItems.ToString()))
            return true;

        foreach (Villager villagerInList in villagers)
        {
            // Comprobar si es igual que otro que ya existe
            villagerInListItems = villagerInList.items;

            if (thisVillagerItems.ToString().Equals(villagerInListItems.ToString()))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Método AddRobbery, que resta un robo al ladrón
    /// </summary>
    public void AddRobbery()
    {
        thiefRobberies++;
        UIManager.instance.UpdateRobberiesText(thiefRobberies, easyDifficulty.thiefRobberies);
        if (thiefRobberies == easyDifficulty.thiefRobberies)
            EndGameAsLose();
    }

    /// <summary>
    /// Método AddAttempt, que resta un intento fallido
    /// </summary>
    public void AddAttempt()
    {
        attemptsCount--;
        UIManager.instance.UpdateAttempts(attemptsCount);
        if (attemptsCount == 0)
            EndGameAsLose();
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
        //UIManager.instance.UpdateEndGameScreen(true, endGameTime - startGameTime);

        // Acabar la partida
        Invoke("EndGame", endTimeWait);
    }

    /// <summary>
    /// Método EndGameAsLose, que acaba la partida como derrota
    /// </summary>
    public void EndGameAsLose()
    {
        // Actualizar UI por derrota
        UIManager.instance.UpdateEndGameScreen(false, 0);

        // Acabar la partida
        Invoke("EndGame", endTimeWait);
    }

    /// <summary>
    /// Método EndGame, que acaba la partida
    /// </summary>
    public void EndGame()
    {
        // Se pausa la partida
        PauseGame();

        // Mostrar pantalla final
        levelLoader.LoadCanvas(3);
        levelLoader.UseCircle(true);
        levelLoader.ShowBars(true);
    }

    /// <summary>
    /// Método UpdateRobberiesTranslate, que actualiza traducido el texto de los robos
    /// </summary>
    public void UpdateRobberiesTranslate()
    {
        if (UIManager.instance != null && easyDifficulty != null)
            UIManager.instance.UpdateRobberiesText(thiefRobberies, easyDifficulty.thiefRobberies);
    }
    #endregion
}