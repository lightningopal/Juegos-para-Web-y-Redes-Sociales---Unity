using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialUIManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    public static TutorialUIManager instance;

    [Header("Textos")]
    [Tooltip("Texto que cuenta los robos")]
    [SerializeField]
    private TextMeshProUGUI robberiesText = null;

    [Header("Corazones")]
    [Tooltip("Sprite del corazón vacío")]
    [SerializeField]
    private Sprite attemptsEmptyHeartSprite = null;
    [Tooltip("GameObjects de los corazones")]
    [SerializeField]
    private GameObject[] attemptsHearts = new GameObject[3];

    [Header("EndGame")]
    [Tooltip("Texto del endgame")]
    [SerializeField]
    private TextMeshProUGUI endGameText = null;
    [Tooltip("Contenedor para los VFX")]
    [SerializeField]
    private GameObject endGameVFX = null;

    [Header("Robos")]
    [Tooltip("Objeto padre de los robos")]
    [SerializeField]
    private GameObject robberiesParent = null;
    [Tooltip("Prefab de los robos")]
    [SerializeField]
    private GameObject robberyPrefab = null;
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
    /// Método UpdateAttempts, que actualiza los intentos restantes
    /// </summary>
    /// <param name="attempts">Intentos restantes</param>
    public void UpdateAttempts(int attempts)
    {
        // Cambiamos el sprite del último corazón
        attemptsHearts[attempts].GetComponent<Image>().sprite = attemptsEmptyHeartSprite;
    }

    /// <summary>
    /// Método UpdateEndGameScreen, que actualiza la UI de la pantalla final
    /// </summary>
    public void UpdateEndGameScreen()
    {
        // Texto de arriba
        endGameText.text = LocalizationSystem.GetLocalizedValue("ENDGAME_WIN");

        // Efecto de victoria
        GameObject victoryVFX = Instantiate(TutorialGameManager.instance.victoryVFX, endGameVFX.transform);
    }

    public void ShowRobberyIcon(Vector3 location)
    {
        GameObject robberyGameObject = Instantiate(robberyPrefab, robberiesParent.transform);
        Robbery newRobbery = robberyGameObject.GetComponent<Robbery>();
        newRobbery.robberyPosition = location;
        newRobbery.robberyRectTransform.anchoredPosition = new Vector2(100000, 100000);
        TutorialGameManager.instance.robberies.Add(newRobbery);
    }

    public void HideRobberyIcon(Robbery robbery)
    {
        // Borramos el robo de la lista de robos
        TutorialGameManager.instance.robberies.Remove(robbery);

        // Eliminamos el GameObject del robo
        Destroy(robbery.gameObject);
    }
    #endregion
}