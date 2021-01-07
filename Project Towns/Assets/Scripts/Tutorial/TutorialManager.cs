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

    [Tooltip("GameObjects de las flechas de rotación de la cámara")]
    [SerializeField]
    private GameObject[] cameraArrows = new GameObject[2];

    [Tooltip("GameObjects de los intentos")]
    [SerializeField]
    private GameObject[] heartsUI = new GameObject[5];

    [Tooltip("Evento actual")]
    public int actualEvent = 0;
    [Tooltip("Texto actual")]
    public int actualText = 0;

    [Tooltip("Texto del tutorial")]
    [SerializeField]
    private TextMeshProUGUI tutorialText = null;
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
        //UpdateTutorialTranslate();
    }
    #endregion

    #region MétodosClase
    public void ShowNextText()
    {
        actualText++;
        UpdateTutorialTranslate();
    }

    public void ShowNextEvent()
    {
        actualEvent++;

        /*switch (actualEvent)
        {

        }*/
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
    #endregion
}
