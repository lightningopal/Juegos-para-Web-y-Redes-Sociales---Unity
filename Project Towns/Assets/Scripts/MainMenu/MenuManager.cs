using UnityEngine;

/// <summary>
/// Clase MenuManager, que controla el menú del juego
/// </summary>
public class MenuManager : MonoBehaviour
{
    #region Variables
    [Tooltip("LevelLoader")]
    [SerializeField]
    private LevelLoader levelLoader = null;

    [Header("Créditos")]
    [Tooltip("Imágenes del paralaje")]
    [SerializeField]
    private RectTransform[] parallaxImages = new RectTransform[4];
    [Tooltip("Imagen del texto de los créditos")]
    [SerializeField]
    private RectTransform creditsText = null;
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

    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        foreach (Audio a in AudioManager.instance.music)
        {
            AudioManager.instance.ManageAudio(a.name, "music", "stop");
            AudioManager.instance.ManageAudio(a.name, "music", "unpause");
        }
        //AudioManager.instance.ManageAudio("MainTheme", "music", "play");

        creditsTextPosition = creditsText.localPosition;
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
        creditsText.localPosition = new Vector3(creditsText.localPosition.x, creditsText.localPosition.y + (creditsSpeed * Time.deltaTime), 0);
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método StartCredits, que inicia los créditos
    /// </summary>
    public void StartCredits()
    {
        creditsText.localPosition = creditsTextPosition;
    }

    /// <summary>
    /// Método StartGameWithDifficulty, que empieza una nueva partida con la dificultad dada
    /// </summary>
    /// <param name="difficulty">Dificultad de la partida</param>
    public void StartGameWithDifficulty(int difficulty)
    {
        GlobalVars.instance.difficulty = difficulty;
        levelLoader.LoadScene(0);
    }
    #endregion
}
