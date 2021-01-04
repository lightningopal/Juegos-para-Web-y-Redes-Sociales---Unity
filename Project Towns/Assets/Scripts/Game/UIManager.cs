using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    public static UIManager instance;

    [Header("Textos")]
    [Tooltip("Texto que cuenta los robos")]
    [SerializeField]
    private TextMeshProUGUI robberiesText = null;

    [Header("Background Corazones")]
    [Tooltip("RectTransform del fondo de los intentos")]
    [SerializeField]
    private RectTransform attemptsBackground = null;
    [Tooltip("Lista de tamaños del background")]
    [SerializeField]
    private float[] backgroundWidths = new float[3];

    [Header("Corazones")]
    [Tooltip("Sprite del corazón vacío")]
    [SerializeField]
    private Sprite attemptsEmptyHeartSprite = null;
    [Tooltip("GameObjects de los corazones")]
    [SerializeField]
    private GameObject[] attemptsHearts = new GameObject[3];

    [Header("EndGame")]
    [Tooltip("Imagen de Marshallow")]
    [SerializeField]
    private Image marshallowImage = null;
    [Tooltip("Sprite de Marshallow al ganar")]
    [SerializeField]
    private Sprite winMarshallowSprite = null;
    [Tooltip("Sprite de Marshallow al perder")]
    [SerializeField]
    private Sprite loseMarshallowSprite = null;
    [Tooltip("Texto del endgame")]
    [SerializeField]
    private TextMeshProUGUI endGameText = null;
    [Tooltip("Texto del tiempo")]
    [SerializeField]
    private TextMeshProUGUI timeText = null;
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
    /// Método UpdateRobberiesText, que actualiza el texto correspondiente a los robos restantes
    /// </summary>
    /// <param name="robberiesLeft">Contador de robos</param>
    /// <param name="maxRobberies">Máximo de robos</param>
    public void UpdateRobberiesText(int robberiesLeft, int maxRobberies)
    {
        robberiesText.text = LocalizationSystem.GetLocalizedValue("THIEF_ROBBERIES") +
            ": " + robberiesLeft + "/" + maxRobberies;
    }

    /// <summary>
    /// Método InitializeAttempts, que dibuja los intentos al empezar
    /// </summary>
    /// <param name="attempts"></param>
    public void InitializeAttempts(Difficulty difficulty)
    {
        // Desactivamos los corazones innecesarios
        for (int i = difficulty.catchAttempts; i < attemptsHearts.Length; i++)
        {
            attemptsHearts[i].SetActive(false);
        }

        // Reescalamos el background de los corazones
        //attemptsBackground.sizeDelta = new Vector2(backgroundWidths[difficulty.difficultyIndex], attemptsBackground.rect.height);
        attemptsBackground.localScale = new Vector3(backgroundWidths[difficulty.difficultyIndex], 1.0f, 1.0f);
    }

    /// <summary>
    /// Método UpdateAttempts, que actualiza los intentos restantes
    /// </summary>
    /// <param name="attempts">Intentos restantes</param>
    public void UpdateAttempts(int attempts)
    {
        // Cambiamos el sprite del último corazón
        attemptsHearts[attempts].GetComponent<SpriteRenderer>().sprite = attemptsEmptyHeartSprite;
    }

    /// <summary>
    /// Método UpdateEndGameScreen, que actualiza la UI de la pantalla final
    /// </summary>
    /// <param name="hasWin">Booleano que indica si ha ganado o perdido</param>
    /// <param name="totalTime">Tiempo total de la partida</param>
    public void UpdateEndGameScreen(bool hasWin, float totalTime)
    {
        // Si ha ganado
        if (hasWin)
        {
            // Texto de arriba
            endGameText.text = LocalizationSystem.GetLocalizedValue("ENDGAME_WIN");

            // Sprite Marshallow
            marshallowImage.sprite = winMarshallowSprite;

            // Texto del tiempo
            int seconds = (int)(totalTime % 60);
            int minutes = (int)((totalTime / 60) % 60);
            string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);

            timeText.text = LocalizationSystem.GetLocalizedValue("TIME") + ": " + timerString;
        }
        // Si ha perdido
        else
        {
            // Texto de arriba
            endGameText.text = LocalizationSystem.GetLocalizedValue("ENDGAME_LOSE");

            // Sprite Marshallow
            marshallowImage.sprite = loseMarshallowSprite;

            // Texto del tiempo
            timeText.gameObject.SetActive(false);
        }
    }

    // Esto me tengo que esperar a tener claro como sería la UI
    public void ShowRobberyIcon(Vector3 location)
    {

    }

    /*public void HideRobberyIcon(Steal steal)
    {

    }*/
    #endregion
}