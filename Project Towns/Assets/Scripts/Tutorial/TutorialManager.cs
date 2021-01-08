using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    #region Variables
    [Tooltip("Singleton")]
    public static TutorialManager instance;

    [Tooltip("Booleano que indica si el jugador puede jugar")]
    public bool playerCanPlay = false;

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

    [Header("Variables")]
    [Tooltip("Evento actual")]
    public int actualEvent = 0;
    [Tooltip("Texto actual")]
    public int actualText = 0;

    [Header("Eventos")]
    [Tooltip("Textos antes del evento")]
    [HideInInspector]
    public int[] textsUntilEvent = { 0, 4, 4, 5, 6, 6, 7, 7, 8, 11, 13, 19, 20, 28, 36, 39, 40, 47};
    //[HideInInspector]
    [Tooltip("Eventos activados")]
    public bool[] activatedEvents = new bool[18];

    [Header("Variables de eventos")]
    [Tooltip("Contador de pasos del evento 1")]
    public int event1Moves = 0;
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
                Event2();
                break;
            case 3:
                Event3();
                break;
            case 4:
                Event4();
                break;
            case 5:
                Event5();
                break;
            case 6:
                Event6();
                break;
            case 7:
                Event7();
                break;
            case 8:
                Event8();
                break;
            case 9:
                Event9();
                break;
            case 10:
                Event10();
                break;
            case 11:
                Event11();
                break;
            case 12:
                Event12();
                break;
            case 13:
                Event13();
                break;
            case 14:
                Event14();
                break;
            case 15:
                Event15();
                break;
            case 16:
                Event16();
                break;
            case 17:
                Event17();
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

        // Dejamos al jugador que juegue
        playerCanPlay = true;
    }

    /// <summary>
    /// Método Event2, que ejecuta la funcionalidad requerida por el evento 2
    /// </summary>
    public void Event2()
    {
        // Mostramos el texto del tutorial
        marshugusGameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);

        // Impedimos al jugador que juegue
        playerCanPlay = false;

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

    }

    /// <summary>
    /// Método Event5, que ejecuta la funcionalidad requerida por el evento 5
    /// </summary>
    public void Event5()
    {

    }

    /// <summary>
    /// Método Event6, que ejecuta la funcionalidad requerida por el evento 6
    /// </summary>
    public void Event6()
    {

    }

    /// <summary>
    /// Método Event7, que ejecuta la funcionalidad requerida por el evento 7
    /// </summary>
    public void Event7()
    {

    }

    /// <summary>
    /// Método Event8, que ejecuta la funcionalidad requerida por el evento 8
    /// </summary>
    public void Event8()
    {

    }

    /// <summary>
    /// Método Event9, que ejecuta la funcionalidad requerida por el evento 9
    /// </summary>
    public void Event9()
    {

    }

    /// <summary>
    /// Método Event10, que ejecuta la funcionalidad requerida por el evento 10
    /// </summary>
    public void Event10()
    {

    }

    /// <summary>
    /// Método Event11, que ejecuta la funcionalidad requerida por el evento 11
    /// </summary>
    public void Event11()
    {

    }

    /// <summary>
    /// Método Event12, que ejecuta la funcionalidad requerida por el evento 12
    /// </summary>
    public void Event12()
    {

    }

    /// <summary>
    /// Método Event13, que ejecuta la funcionalidad requerida por el evento 13
    /// </summary>
    public void Event13()
    {

    }

    /// <summary>
    /// Método Event14, que ejecuta la funcionalidad requerida por el evento 14
    /// </summary>
    public void Event14()
    {

    }

    /// <summary>
    /// Método Event15, que ejecuta la funcionalidad requerida por el evento 15
    /// </summary>
    public void Event15()
    {

    }

    /// <summary>
    /// Método Event16, que ejecuta la funcionalidad requerida por el evento 16
    /// </summary>
    public void Event16()
    {

    }

    /// <summary>
    /// Método Event17, que ejecuta la funcionalidad requerida por el evento 17
    /// </summary>
    public void Event17()
    {

    }
    #endregion

    /// <summary>
    /// Método UpdateTutorialTranslate, que actualiza el texto correspondiente al tutorial
    /// </summary>
    public void UpdateTutorialTranslate()
    {
        string textSt = LocalizationSystem.GetLocalizedValue("TUTORIAL_TEXT_" + actualText);
        textSt = textSt.Replace("\\n", "\n");
        tutorialText.text = textSt;
    }
    #endregion
}
