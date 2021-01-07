using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    #region Variables
    [Tooltip("Singleton")]
    public static TutorialManager instance;

    [Tooltip("GameObjects de las flechas de rotación de la cámara")]
    [SerializeField]
    private GameObject[] cameraArrows = new GameObject[2];

    private int actualEvent = 0;
    private int actualText = 0;

    private TextMeshProUGUI tutorialText;
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
        tutorialText.text = LocalizationSystem.GetLocalizedValue("TUTORIAL_TEXT_" + actualText);
    }
    #endregion

    #region MétodosClase
    public void ShowNextText()
    {
        actualText++;
        tutorialText.text = LocalizationSystem.GetLocalizedValue("TUTORIAL_TEXT_" + actualText);
    }

    public void ShowNextEvent()
    {
        actualEvent++;

        /*switch (actualEvent)
        {

        }*/
    }
    #endregion
}
