using UnityEngine;
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

    [Header("Imágenes")]
    [Tooltip("Sprite del fondo de los intentos")]
    [SerializeField]
    private Sprite attemptsBackgroundSprite = null;
    [Tooltip("Sprite del corazón lleno")]
    [SerializeField]
    private Sprite attemptsFilledHeartSprite = null;
    [Tooltip("Sprite del corazón vacío")]
    [SerializeField]
    private Sprite attemptsEmptyHeartSprite = null;

    [Tooltip("GameObjects de los corazones")]
    [SerializeField]
    private GameObject[] attemptsHearts = new GameObject[3];
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
        robberiesText.text = LocalizationSystem.GetLocalizedValue("REMAINING_ROBBERIES") +
            ": " + robberiesLeft + "/" + maxRobberies;
    }

    /// <summary>
    /// Método InitializeAttempts, que dibuja los intentos al empezar
    /// </summary>
    /// <param name="attempts"></param>
    public void InitializeAttempts(int attempts)
    {
        // Desactivamos los corazones innecesarios
        for (int i = attempts; i < attemptsHearts.Length; i++)
        {
            attemptsHearts[i].SetActive(false);
        }
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

    // Esto me tengo que esperar a tener claro como sería la UI
    public void ShowRobberyIcon()
    {

    }

    public void HideRobberyIcon()
    {

    }
    #endregion
}