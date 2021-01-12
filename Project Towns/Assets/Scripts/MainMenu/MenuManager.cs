using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Clase MenuManager, que controla el menú del juego
/// </summary>
public class MenuManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    public static MenuManager instance;

    [Header("Créditos")]
    [Tooltip("Imágenes del paralaje")]
    [SerializeField]
    private RectTransform[] parallaxImages = new RectTransform[4];
    [Tooltip("Imagen del texto de los créditos")]
    [SerializeField]
    private Image creditsTextImage = null;
    [Tooltip("Sprites traducidos del texto de los créditos")]
    [SerializeField]
    private Sprite[] translatedCreditsSprites = new Sprite[2];
    [Tooltip("Velocidad de los créditos")]
    [SerializeField]
    private float creditsSpeed = 2.0f;
    [Tooltip("Imagen del texto de los créditos")]
    private Vector3 creditsTextPosition = new Vector3();
    [Tooltip("Factor de paralaje")]
    [SerializeField]
    private float parallaxFactor = 2f;

    [Tooltip("Posición en X del ratón")]
    private float mousePosX;
    [Tooltip("Posición en Y del ratón")]
    private float mousePosY;

    [Header("Pantalla inicio")]
    [Tooltip("Texto de la pantalla de inicio")]
    public TextMeshProUGUI startText;
    [Tooltip("Imagen del icono")]
    public Image iconImage;
    [Tooltip("Sprites del icono")]
    public Sprite[] iconSprites = new Sprite[2];

    [Header("Tap to continue")]
    [Tooltip("Level Loader")]
    [SerializeField]
    private LevelLoader levelLoader = null;

    [Tooltip("Botones")]
    [SerializeField]
    private Button[] menuButtons = new Button[2];
    [Tooltip("Botones del menú")]
    [SerializeField]
    private CustomMenuButton[] menuCustomButtons = new CustomMenuButton[2];
    [Tooltip("Textos de tap again")]
    [SerializeField]
    private TextMeshProUGUI[] menuButtonsTapText = new TextMeshProUGUI[2];
    [Tooltip("Botón invisible")]
    [SerializeField]
    private Button invisibleButton = null;

    private int mobileTapped = -1;
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
        Time.timeScale = 1;
        foreach (Audio a in AudioManager.instance.music)
        {
            AudioManager.instance.ManageAudio(a.name, "music", "stop");
            AudioManager.instance.ManageAudio(a.name, "music", "unpause");
        }
        AudioManager.instance.ManageAudio("MenuMusic", "music", "play");

        creditsTextPosition = creditsTextImage.rectTransform.localPosition;

        // Texto inicio
        UpdateStartTranslate();
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        //Mover el fondo
        mousePosX = Input.mousePosition.x;
        mousePosY = Input.mousePosition.y;

        for (int i = 0; i <parallaxImages.Length; i++)
        {
            float localParallaxFactor = parallaxFactor * (i + 1);
            parallaxImages[i].localPosition= new Vector2(
                (mousePosX / Screen.width) * localParallaxFactor,
                (mousePosY / Screen.height) * localParallaxFactor);
        }

        // Mover los créditos
        if (creditsTextImage.rectTransform.localPosition.y < 2500)
            creditsTextImage.rectTransform.localPosition = new Vector3(
                creditsTextImage.rectTransform.localPosition.x,
                creditsTextImage.rectTransform.localPosition.y + (creditsSpeed * Time.deltaTime), 0);
            
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método StartCredits, que inicia los créditos
    /// </summary>
    public void StartCredits()
    {
        switch (LocalizationSystem.language)
        {
            case LocalizationSystem.Language.English:
                creditsTextImage.sprite = translatedCreditsSprites[0];
                break;
            case LocalizationSystem.Language.Spanish:
                creditsTextImage.sprite = translatedCreditsSprites[1];
                break;
            default:
                creditsTextImage.sprite = translatedCreditsSprites[0];
                break;
        }

        creditsTextImage.rectTransform.localPosition = creditsTextPosition;
    }

    /// <summary>
    /// Método UpdateStartTranslate, que actualiza traducido el texto del inicio
    /// </summary>
    public void UpdateStartTranslate()
    {
        if (Application.isMobilePlatform)
        {
            iconImage.sprite = iconSprites[1];
            startText.text = LocalizationSystem.GetLocalizedValue("START_MOBILE");
        }
        else
        {
            iconImage.sprite = iconSprites[0];
            startText.text = LocalizationSystem.GetLocalizedValue("START_DESKTOP");
        }
    }
    
    public void SelectMode(int mode)
    {
        /*if (!Application.isMobilePlatform)
        {
            optionsManager.StartGameWithDifficulty(difficulty);
        }
        else
        {*/
        // Si ya ha hecho tap
        if (mobileTapped == mode)
        {
            invisibleButton.Select();
            if (mobileTapped == 0)
            {
                levelLoader.LoadCanvas(4);
                levelLoader.UseCircle(false);
            }
            else if (mobileTapped == 1)
            {
                levelLoader.LoadScene(2);
                levelLoader.UseCircle(true);
            }
            mobileTapped = -1;
        }
        // Si no
        else
        {
            mobileTapped = mode;

            for (int i = 0; i < menuButtonsTapText.Length; i++)
            {
                if (i == mode)
                {
                    menuButtonsTapText[i].gameObject.SetActive(true);
                    menuButtons[i].Select();
                    menuCustomButtons[i].isSelected = true;
                }
            }
        }
        //}
    }

    public void ResetMobileTap()
    {
        mobileTapped = -1;
    }
    #endregion
}
