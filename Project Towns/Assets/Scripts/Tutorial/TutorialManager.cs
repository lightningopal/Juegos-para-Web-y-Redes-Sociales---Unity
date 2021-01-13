using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    public static TutorialManager instance;

    [Header("Variables de control")]
    [Tooltip("Booleano que indica si el jugador puede moverse")]
    [HideInInspector]
    public bool playerCanMove = false;
    [Tooltip("Booleano que indica si el jugador puede usar la UI")]
    [HideInInspector]
    public bool playerCanUseUI = false;
    [Tooltip("Booleano que indica si el jugador tiene que esperar")]
    [HideInInspector]
    public bool playerHasToWait = false;
    [Tooltip("Booleano que indica si el jugador puede volver al menú principal")]
    [HideInInspector]
    public bool event28CanReturnToMenu = false;

    [Header("GameObjects y Referencias")]
    [Tooltip("GameObject de las flechas de rotación de la cámara")]
    [SerializeField]
    private GameObject cameraButtons = null;
    [Tooltip("GameObjects de los intentos")]
    [SerializeField]
    private GameObject[] heartsUI = new GameObject[5];
    [Tooltip("GameObject del cuadro de Marshugus")]
    [SerializeField]
    private GameObject marshugusGameObject = null;
    [Tooltip("Texto del tutorial")]
    [SerializeField]
    private TextMeshProUGUI tutorialText = null;
    [Tooltip("Level Loader")]
    [SerializeField]
    private LevelLoader levelLoader = null;
    [Tooltip("GameObject del EndGame")]
    [SerializeField]
    private GameObject endGameGameObject = null;
    [Tooltip("GameObject del VFX del EndGame")]
    [SerializeField]
    private GameObject endGameVFX = null;
    [Tooltip("Objeto padre de los robos")]
    [SerializeField]
    private GameObject robberiesParent = null;
    [Tooltip("Prefab de los robos")]
    [SerializeField]
    private GameObject robberyPrefab = null;
    [Tooltip("Sprite de la flecha")]
    [SerializeField]
    private Sprite arrowSprite = null;

    [Header("Prefabs y parents")]
    [Tooltip("Prefab del aldeano scripteado")]
    [SerializeField]
    private GameObject scriptedVillagerPrefab = null;
    [Tooltip("Objeto padre de los aldeanos")]
    [SerializeField]
    private GameObject villagersParent = null;
    [Tooltip("Ladrón scripted")]
    private ScriptedVillager scriptedThief = null;

    [Header("Variables")]
    [Tooltip("Evento actual")]
    [HideInInspector]
    public int actualEvent = 0;
    [HideInInspector]
    [Tooltip("Texto actual")]
    public int actualText = 0;

    [Header("Eventos")]
    [Tooltip("Textos antes del evento")]
    private int[] textsUntilEvent = { 0, 4, 4, 5, 6, 6, 7, 7, 8, 11, 11, 13, 13, 19, 20, 20, 28, 28, 36, 36, 39, 39, 40, 40, 42, 47, 47, 47, 54};
    [HideInInspector]
    [Tooltip("Eventos activados")]
    public bool[] activatedEvents = new bool[29];

    [Header("Variables de eventos")]
    [Tooltip("Contador de pasos del evento 1")]
    [HideInInspector]
    public int event1Moves = 0;
    [Tooltip("Booleano que indica que ha movido la cámara en sentido antihorario")]
    [HideInInspector]
    public bool event4Antihorary = false;
    [Tooltip("Booleano que indica que ha movido la cámara en sentido horario")]
    [HideInInspector]
    public bool event4Horary = false;
    [Tooltip("Booleano que indica si el ID1 ha dado ya la info")]
    [HideInInspector]
    public bool id1HasAlreadyGivenInfo = false;
    [Tooltip("Booleano que indica si el ID3 ha dado ya la info")]
    [HideInInspector]
    public bool id3HasAlreadyGivenInfo = false;
    [Tooltip("Booleano que indica si debe pulsar sobre el aldeano con cuernos")]
    [HideInInspector]
    public bool event20MustClickHornsVillager = false;
    [Tooltip("Booleano que indica si debe detener al aldeano con cuernos")]
    [HideInInspector]
    public bool event22MustArrestHornsVillager = false;
    [Tooltip("Booleano que indica si debe pulsar sobre el ladrón")]
    [HideInInspector]
    public bool event25MustClickThief = false;
    [Tooltip("Booleano que indica si debe detener al ladrón")]
    [HideInInspector]
    public bool event26MustArrestThief = false;
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
        activatedEvents[0] = true;

        /// Instanciar ladrón
        // Obtener datos aleatorios
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        // Lo instanciamos
        GameObject thiefGameObject = Instantiate(scriptedVillagerPrefab, new Vector3(-11.5f, 4.65f, -4.2f), randomRotation, villagersParent.transform); ;

        // Obtener componente ScriptedVillager
        scriptedThief = thiefGameObject.GetComponent<ScriptedVillager>();

        // Establecemos sus objetos
        scriptedThief.items.villagerColor = ItemDatabase.instance.characterColors[0];
        scriptedThief.items.eyes = ItemDatabase.instance.eyes[1];
        scriptedThief.items.hatItem = ItemDatabase.instance.hatItems[0];
        scriptedThief.items.hornItem = ItemDatabase.instance.noItems[1];
        scriptedThief.items.neckItem = ItemDatabase.instance.neckItems[0];

        // Instanciamos sus objetos
        scriptedThief.PutItems();

        // Cambiamos su tag
        scriptedThief.tag = "ScriptedThief";

        // Establecemos su id de tutorial
        scriptedThief.tutorialID = 5;

        // Lo desactivamos
        scriptedThief.gameObject.SetActive(false);
    }
    #endregion

    #region MétodosClase
    public void GoToNextStep()
    {
        // Si toca texto
        if (actualText < textsUntilEvent[actualEvent + 1])
        {
            ShowNextText();
        }
        // Si toca evento
        else
        {
            ShowNextEvent();
        }
    }

    public void ShowNextText()
    {
        actualText++;
        UpdateTutorialTranslate();
    }

    public void ShowNextEvent()
    {
        actualEvent++;

        // Ejecutar el evento
        switch (actualEvent)
        {
            case 1:
                Event1();
                break;
            case 2:
                activatedEvents[2] = true;
                Event2();
                break;
            case 3:
                activatedEvents[3] = true;
                Event3();
                break;
            case 4:
                Event4();
                break;
            case 5:
                activatedEvents[5] = true;
                Event5();
                break;
            case 6:
                Event6();
                break;
            case 7:
                activatedEvents[7] = true;
                Event7();
                break;
            case 8:
                activatedEvents[8] = true;
                Event8();
                break;
            case 9:
                Event9();
                break;
            case 10:
                activatedEvents[10] = true;
                Event10();
                break;
            case 11:
                Event11();
                break;
            case 12:
                activatedEvents[12] = true;
                Event12();
                break;
            case 13:
                activatedEvents[13] = true;
                Event13();
                break;
            case 14:
                Event14();
                break;
            case 15:
                activatedEvents[15] = true;
                Event15();
                break;
            case 16:
                Event16();
                break;
            case 17:
                activatedEvents[17] = true;
                Event17();
                break;
            case 18:
                Event18();
                break;
            case 19:
                activatedEvents[19] = true;
                Event19();
                break;
            case 20:
                Event20();
                break;
            case 21:
                activatedEvents[21] = true;
                Event21();
                break;
            case 22:
                Event22();
                break;
            case 23:
                activatedEvents[23] = true;
                Event23();
                break;
            case 24:
                activatedEvents[24] = true;
                Event24();
                break;
            case 25:
                Event25();
                break;
            case 26:
                Event26();
                break;
            case 27:
                activatedEvents[27] = true;
                Event27();
                break;
            case 28:
                activatedEvents[28] = true;
                Event28();
                break;
        }
    }

    #region Eventos
    /// <summary>
    /// Método Event1, que ejecuta la funcionalidad requerida por el evento 1
    /// </summary>
    public void Event1()
    {
        // Ocultamos el texto del tutorial
        marshugusGameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);

        // Dejamos al jugador que se mueva
        playerCanMove = true;
    }

    /// <summary>
    /// Método Event2, que ejecuta la funcionalidad requerida por el evento 2
    /// </summary>
    public void Event2()
    {
        // Mostramos el texto del tutorial
        marshugusGameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);

        // Impedimos al jugador que se mueva
        playerCanMove = false;

        // Mostramos siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event3, que ejecuta la funcionalidad requerida por el evento 3
    /// </summary>
    public void Event3()
    {
        cameraButtons.SetActive(true);

        // Mostramos siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event4, que ejecuta la funcionalidad requerida por el evento 4
    /// </summary>
    public void Event4()
    {
        // Cambiamos la variable playerCanUseUI a true
        playerCanUseUI = true;
    }

    /// <summary>
    /// Método Event5, que ejecuta la funcionalidad requerida por el evento 5
    /// </summary>
    public void Event5()
    {
        // Cambiamos la variable playerCanUseUI a false
        playerCanUseUI = false;

        // Mostramos siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event6, que ejecuta la funcionalidad requerida por el evento 6
    /// </summary>
    public void Event6()
    {
        // Ocultamos el texto del tutorial
        marshugusGameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);

        // Dejamos al jugador que se mueva
        playerCanMove = true;

        // Establecemos un "robo" que hace de flecha para la forja
        GameObject robberyGameObject = Instantiate(robberyPrefab, robberiesParent.transform);
        Robbery newRobbery = robberyGameObject.GetComponent<Robbery>();
        newRobbery.robberyPosition = new Vector3(12.66f, 7.85f, 30.85f);
        newRobbery.robberyRectTransform.anchoredPosition = new Vector2(100000, 100000);
        newRobbery.robberyIconImage.sprite = arrowSprite;
        robberyGameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        TutorialGameManager.instance.robberies.Add(newRobbery);
    }

    /// <summary>
    /// Método Event7, que ejecuta la funcionalidad requerida por el evento 7
    /// </summary>
    public void Event7()
    {
        // Mostramos el texto del tutorial
        marshugusGameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);

        // Impedimos al jugador que se mueva
        playerCanMove = false;

        // Mostramos siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event8, que ejecuta la funcionalidad requerida por el evento 8
    /// </summary>
    public void Event8()
    {
        /// Creamos una víctima en el ayuntamiento
        // Obtener datos aleatorios
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        // La instanciamos
        GameObject villagerGameObject = Instantiate(scriptedVillagerPrefab, new Vector3(-48.5f, 11.5f, 27.5f), randomRotation, villagersParent.transform); ;

        // Obtener componente ScriptedVillager
        ScriptedVillager townHallScriptedVillager = villagerGameObject.GetComponent<ScriptedVillager>();

        // Aleatorizamos el aldeano
        do
        {
            townHallScriptedVillager.RandomizeNPC();
        } while (townHallScriptedVillager.items.ToString().Equals(scriptedThief.items.ToString()));
        

        // Instanciamos sus objetos
        townHallScriptedVillager.PutItems();

        // Añadimos al aldeano a la lista
        TutorialGameManager.instance.villagers.Add(townHallScriptedVillager);

        // Establecemos los dos objetos que ha visto
        townHallScriptedVillager.informationGameObject.item1 = ItemDatabase.instance.characterColors[0];
        townHallScriptedVillager.informationGameObject.item2 = ItemDatabase.instance.eyes[1];

        // Establecemos que sea víctima
        townHallScriptedVillager.GetRobbed();

        // Establecemos su id de tutorial
        townHallScriptedVillager.tutorialID = 0;

        /// Creamos un robo donde la víctima
        TutorialUIManager.instance.ShowRobberyIcon(villagerGameObject.transform.position);
        TutorialGameManager.instance.AddRobbery();
        //Efecto de sonido
        AudioManager.instance.PlaySound("TheftFound");

        // Mostramos siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event9, que ejecuta la funcionalidad requerida por el evento 9
    /// </summary>
    public void Event9()
    {
        // Ocultamos el texto del tutorial
        marshugusGameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);

        // Dejamos al jugador que se mueva
        playerCanMove = true;
    }

    /// <summary>
    /// Método Event10, que ejecuta la funcionalidad requerida por el evento 10
    /// </summary>
    public void Event10()
    {
        // Mostramos el texto del tutorial
        marshugusGameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);

        // Impedimos al jugador que se mueva
        playerCanMove = false;

        // Mostramos siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event11, que ejecuta la funcionalidad requerida por el evento 11
    /// </summary>
    public void Event11()
    {
        /// Creamos testigo para acercarse al usuario
        // Obtener datos aleatorios
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        // La instanciamos
        GameObject villagerGameObject = Instantiate(scriptedVillagerPrefab, new Vector3(-47.6f, 7.4f, 9.2f), randomRotation, villagersParent.transform); ;

        // Obtener componente ScriptedVillager
        ScriptedVillager secondScriptedVillager = villagerGameObject.GetComponent<ScriptedVillager>();

        // Aleatorizamos el aldeano para que no coincida con el primero
        ScriptedVillager firstVillager = TutorialGameManager.instance.villagers[0];
        do
        {
            secondScriptedVillager.RandomizeNPC();
            
        } while (secondScriptedVillager.items.ToString().Equals(firstVillager.items.ToString())
        || secondScriptedVillager.items.ToString().Equals(scriptedThief.items.ToString()));

        // Instanciamos sus objetos
        secondScriptedVillager.PutItems();

        // Añadimos al aldeano a la lista
        TutorialGameManager.instance.villagers.Add(secondScriptedVillager);

        // Establecemos el objeto que ha visto
        secondScriptedVillager.informationGameObject.item1 = ItemDatabase.instance.hatItems[0];
        secondScriptedVillager.informationGameObject.item2 = null;

        // Establecemos que sea víctima
        secondScriptedVillager.SeeRobbery();

        // Establecemos su id de tutorial
        secondScriptedVillager.tutorialID = 1;

        // Establecemos que el jugador tiene que esperar
        playerHasToWait = true;
    }

    /// <summary>
    /// Método Event12, que ejecuta la funcionalidad requerida por el evento 12
    /// </summary>
    public void Event12()
    {
        // Establecemos que el jugador ya no tiene que esperar
        playerHasToWait = false;

        // Mostramos siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event13, que ejecuta la funcionalidad requerida por el evento 13
    /// </summary>
    public void Event13()
    {
        /// Creamos una víctima en el parque
        // Obtener datos aleatorios
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        // La instanciamos
        GameObject villagerGameObject = Instantiate(scriptedVillagerPrefab, new Vector3(45.5f, 2.8f, -10f), randomRotation, villagersParent.transform); ;

        // Obtener componente ScriptedVillager
        ScriptedVillager parkScriptedVillager = villagerGameObject.GetComponent<ScriptedVillager>();

        // Aleatorizamos el aldeano
        do
        {
            parkScriptedVillager.RandomizeNPC();
        } while (parkScriptedVillager.items.ToString().Equals(scriptedThief.items.ToString()));

        // Instanciamos sus objetos
        parkScriptedVillager.PutItems();

        // Añadimos al aldeano a la lista
        TutorialGameManager.instance.villagers.Add(parkScriptedVillager);

        // Establecemos los dos objetos que ha visto
        parkScriptedVillager.informationGameObject.item1 = ItemDatabase.instance.hornItems[0];
        parkScriptedVillager.informationGameObject.item2 = ItemDatabase.instance.neckItems[0];
        parkScriptedVillager.informationGameObject.doubtfulInformation = true;

        // Establecemos que sea víctima
        parkScriptedVillager.GetRobbed();

        // Establecemos su id de tutorial
        parkScriptedVillager.tutorialID = 2;

        /// Creamos un robo donde la víctima
        TutorialUIManager.instance.ShowRobberyIcon(villagerGameObject.transform.position);
        TutorialGameManager.instance.AddRobbery();
        //Efecto de sonido
        AudioManager.instance.PlaySound("TheftFound");

        // Mostramos siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event14, que ejecuta la funcionalidad requerida por el evento 14
    /// </summary>
    public void Event14()
    {
        // Ocultamos el texto del tutorial
        marshugusGameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);

        // Dejamos al jugador que se mueva
        playerCanMove = true;
    }

    /// <summary>
    /// Método Event15, que ejecuta la funcionalidad requerida por el evento 15
    /// </summary>
    public void Event15()
    {
        // Mostramos el texto del tutorial
        marshugusGameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);

        // Impedimos al jugador que se mueva
        playerCanMove = false;

        // Mostramos siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event16, que ejecuta la funcionalidad requerida por el evento 16
    /// </summary>
    public void Event16()
    {
        /// Creamos testigo para acercarse al usuario
        // Obtener datos aleatorios
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        // La instanciamos
        GameObject villagerGameObject = Instantiate(scriptedVillagerPrefab, new Vector3(51, 2.4f, -30f), randomRotation, villagersParent.transform); ;

        // Obtener componente ScriptedVillager
        ScriptedVillager fourthScriptedVillager = villagerGameObject.GetComponent<ScriptedVillager>();

        // Aleatorizamos el aldeano para que no coincida con el tercero
        ScriptedVillager thirdVillager = TutorialGameManager.instance.villagers[2];
        do
        {
            fourthScriptedVillager.RandomizeNPC();

        } while (fourthScriptedVillager.items.ToString().Equals(thirdVillager.items.ToString())
        || fourthScriptedVillager.items.ToString().Equals(scriptedThief.items.ToString()));

        // Instanciamos sus objetos
        fourthScriptedVillager.PutItems();

        // Añadimos al aldeano a la lista
        TutorialGameManager.instance.villagers.Add(fourthScriptedVillager);

        // Establecemos el objeto que ha visto
        fourthScriptedVillager.informationGameObject.item1 = ItemDatabase.instance.neckItems[0];
        fourthScriptedVillager.informationGameObject.item2 = null;

        // Establecemos que sea víctima
        fourthScriptedVillager.SeeRobbery();

        // Establecemos su id de tutorial
        fourthScriptedVillager.tutorialID = 3;

        // Establecemos que el jugador tiene que esperar
        playerHasToWait = true;
    }

    /// <summary>
    /// Método Event17, que ejecuta la funcionalidad requerida por el evento 17
    /// </summary>
    public void Event17()
    {
        // Establecemos que el jugador ya no tiene que esperar
        playerHasToWait = false;

        // Mostramos siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event18, que ejecuta la funcionalidad requerida por el evento 18
    /// </summary>
    public void Event18()
    {
        // Ocultamos el texto del tutorial
        marshugusGameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);

        // Dejamos al jugador que se mueva
        playerCanMove = true;

        // Establecemos un "robo" que hace de flecha para la plaza
        GameObject robberyGameObject = Instantiate(robberyPrefab, robberiesParent.transform);
        Robbery newRobbery = robberyGameObject.GetComponent<Robbery>();
        newRobbery.robberyPosition = new Vector3(-13.25f, 4.75f, -8f);
        newRobbery.robberyRectTransform.anchoredPosition = new Vector2(100000, 100000);
        newRobbery.robberyIconImage.sprite = arrowSprite;
        robberyGameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        TutorialGameManager.instance.robberies.Add(newRobbery);

        /// Instanciamos aldeano aleatorio y movemos ladrón
        /// Aldeano aleatorio
        // Obtener datos aleatorios
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        // La instanciamos
        GameObject villagerGameObject = Instantiate(scriptedVillagerPrefab, new Vector3(-14.95f, 4.65f, -12f), randomRotation, villagersParent.transform); ;

        // Obtener componente ScriptedVillager
        ScriptedVillager fifthScriptedVillager = villagerGameObject.GetComponent<ScriptedVillager>();

        // Establecemos sus objetos
        fifthScriptedVillager.items.villagerColor = ItemDatabase.instance.characterColors[0];
        fifthScriptedVillager.items.eyes = ItemDatabase.instance.eyes[1];
        fifthScriptedVillager.items.hatItem = ItemDatabase.instance.hatItems[0];
        fifthScriptedVillager.items.hornItem = ItemDatabase.instance.hornItems[0];
        fifthScriptedVillager.items.neckItem = ItemDatabase.instance.neckItems[0];

        // Instanciamos sus objetos
        fifthScriptedVillager.PutItems();

        // Añadimos al aldeano a la lista
        TutorialGameManager.instance.villagers.Add(fifthScriptedVillager);

        // Establecemos su id de tutorial
        fifthScriptedVillager.tutorialID = 4;

        /// Ladrón
        // Lo desactivamos
        scriptedThief.gameObject.SetActive(true);
    }

    /// <summary>
    /// Método Event19, que ejecuta la funcionalidad requerida por el evento 19
    /// </summary>
    public void Event19()
    {
        // Mostramos el texto del tutorial
        marshugusGameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);

        // Impedimos al jugador que se mueva
        playerCanMove = false;

        // Mostramos siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event20, que ejecuta la funcionalidad requerida por el evento 20
    /// </summary>
    public void Event20()
    {
        // Escondemos el texto del tutorial
        marshugusGameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);

        // Permitimos al jugador que se mueva pero que tenga que clickar en el aldeano con cuernos
        playerCanMove = true;
        event20MustClickHornsVillager = true;
    }

    /// <summary>
    /// Método Event21, que ejecuta la funcionalidad requerida por el evento 21
    /// </summary>
    public void Event21()
    {
        // Mostramos el texto del tutorial
        marshugusGameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);

        // Impedimos al jugador moverse
        playerCanMove = false;
        event20MustClickHornsVillager = false;

        // Siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event22, que ejecuta la funcionalidad requerida por el evento 22
    /// </summary>
    public void Event22()
    {
        // Ocultamos el texto del tutorial
        marshugusGameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);

        // Obligamos al jugador a arrestar al aldeano con cuernos
        event22MustArrestHornsVillager = true;
    }

    /// <summary>
    /// Método Event23, que ejecuta la funcionalidad requerida por el evento 23
    /// </summary>
    public void Event23()
    {
        // Mostramos el texto del tutorial
        marshugusGameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);

        // Dejamos de obligar al jugador a arrestar al aldeano con cuernos
        event22MustArrestHornsVillager = false;

        // Siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event24, que ejecuta la funcionalidad requerida por el evento 24
    /// </summary>
    public void Event24()
    {
        // Mostramos la UI de los intentos
        foreach (GameObject g in heartsUI)
        {
            g.SetActive(true);
        }

        // Siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event25, que ejecuta la funcionalidad requerida por el evento 25
    /// </summary>
    public void Event25()
    {
        // Escondemos el texto del tutorial
        marshugusGameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);

        // Permitimos al jugador que se mueva pero que tenga que clickar en el ladrón
        playerCanMove = true;
        event25MustClickThief = true;
    }

    /// <summary>
    /// Método Event26, que ejecuta la funcionalidad requerida por el evento 26
    /// </summary>
    public void Event26()
    {
        // Obligamos al jugador a arrestar al aldeano con cuernos
        playerCanMove = false;
        event25MustClickThief = false;
        event26MustArrestThief = true;
    }

    /// <summary>
    /// Método Event27, que ejecuta la funcionalidad requerida por el evento 27
    /// </summary>
    public void Event27()
    {
        // Mostramos el texto del tutorial
        marshugusGameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);

        // Cambiamos el padre del texto del tutorial
        marshugusGameObject.transform.SetParent(endGameGameObject.transform);
        tutorialText.transform.SetParent(endGameGameObject.transform);

        // Impedimos jugar al jugador
        event26MustArrestThief = false;
        playerCanMove = false;

        // Siguiente paso
        GoToNextStep();
    }

    /// <summary>
    /// Método Event28, que ejecuta la funcionalidad requerida por el evento 28
    /// </summary>
    public void Event28()
    {
        // Escondemos el texto del tutorial
        marshugusGameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);

        // Permitimos que vuelva al menú principal
        event28CanReturnToMenu = true;
    }
    #endregion

    /// <summary>
    /// Método buttonAntihoraryTrue, que indica que se ha girado la cámara en sentido antihorario
    /// </summary>
    public void buttonAntihoraryTrue()
    {
        event4Antihorary = true;
        if (event4Horary && !activatedEvents[4])
        {
            activatedEvents[4] = true;
            GoToNextStep();
        }
    }

    /// <summary>
    /// Método buttonHoraryTrue, que indica que se ha girado la cámara en sentido antihorario
    /// </summary>
    public void buttonHoraryTrue()
    {
        event4Horary = true;
        if (event4Antihorary && !activatedEvents[4])
        {
            activatedEvents[4] = true;
            GoToNextStep();
        }
    }

    /// <summary>
    /// Método UpdateTutorialTranslate, que actualiza el texto correspondiente al tutorial
    /// </summary>
    public void UpdateTutorialTranslate()
    {
        string textSt = LocalizationSystem.GetLocalizedValue("TUTORIAL_TEXT_" + actualText);
        textSt = textSt.Replace("\\n", "\n");
        tutorialText.text = textSt;
    }

    /// <summary>
    /// Método ReturnToMenuButton, que permite volver al menú principal tras el tutorial
    /// </summary>
    public void ReturnToMenuButton()
    {
        if (!event28CanReturnToMenu)
            return;

        levelLoader.LoadScene(0);
        levelLoader.UseCircle(true);
        endGameVFX.gameObject.SetActive(false);
    }
    #endregion
}