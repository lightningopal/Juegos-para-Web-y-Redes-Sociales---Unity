using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Clase OptionsManager, que controla las opciones en cada escena
/// </summary>
public class OptionsManager : MonoBehaviour
{
    #region Variables
    /*[Tooltip("Singleton")]
    public static OptionsManager instance;*/

    [Header("Audio")]
    [Tooltip("Slider música")]
    [SerializeField]
    private Slider musicSlider = null;
    [Tooltip("Slider sonidos")]
    [SerializeField]
    private Slider fxSlider = null;

    //Volumen música
    private float musicVolume;
    //Volumen sonidos
    private float fxVolume;

    [Header("Gráficos")]
    [Tooltip("Slider brillo")]
    [SerializeField]
    private Slider brightnessSlider = null;
    [Tooltip("Imagen del brillo")]
    [SerializeField]
    private Image brightnessImage = null;

    // Nivel brillo
    private float brightnessLvl = 1.0f;

    // Idioma
    [Header("Idioma")]
    [Tooltip("Imagen selectora de idioma")]
    [SerializeField]
    private Image languageSelectedBackground = null;
    [Tooltip("Posiciones background")]
    [SerializeField]
    private Vector2[] backgroundPositions = new Vector2[2];
    [Tooltip("Índice de idioma seleccionado")]
    private int currentLanguageIndex;

    [Header("Otros")]
    [Tooltip("GameObjects de las imágenes de los textos")]
    [SerializeField]
    private GameObject[] textImageGameObjects = new GameObject[3];
    [Tooltip("Textos de las opciones")]
    [SerializeField]
    private TextMeshProUGUI[] optionsTexts = new TextMeshProUGUI[3];
    [Tooltip("Level Loader")]
    [SerializeField]
    private LevelLoader levelLoader = null;

    [Header("Colores")]
    [Tooltip("Color del texto normal")]
    [SerializeField]
    private Color normalColor = new Color();
    [Tooltip("Color del texto seleccionado")]
    [SerializeField]
    private Color highlightedColor = new Color(255, 255, 255, 1);

    // Escena actual
    private Scene actualScene;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        //instance = this;

        actualScene = SceneManager.GetActiveScene();

        musicVolume = GlobalVars.instance.musicVolume;
        fxVolume = GlobalVars.instance.fxVolume;
        brightnessLvl = GlobalVars.instance.brightnessLvl;

        musicSlider.value = musicVolume;
        fxSlider.value = fxVolume;

        // Brillo
        if (brightnessLvl > 0.5)
            brightnessImage.color = new Vector4(1.0f, 1.0f, 1.0f, brightnessLvl - 0.5f);
        else
            brightnessImage.color = new Vector4(0.0f, 0.0f, 0.0f, 0.5f - brightnessLvl);

        brightnessSlider.value = brightnessLvl;

        // Idioma
        currentLanguageIndex = (int)LocalizationSystem.language;
        //languageSelectedBackground.rectTransform.localPosition = new Vector3(languageSelectedBackground.rectTransform.localPosition.x, backgroundPositions[currentLanguageIndex], 0);
        // Se mueven los puntos de anclaje (que son independientes del tamaño de la pantalla, van de 0 a 1) y se centra la imagen
        languageSelectedBackground.rectTransform.anchorMin = new Vector2(languageSelectedBackground.rectTransform.anchorMin.x, backgroundPositions[currentLanguageIndex][0]);
        languageSelectedBackground.rectTransform.anchorMax = new Vector2(languageSelectedBackground.rectTransform.anchorMax.x, backgroundPositions[currentLanguageIndex][1]);
        languageSelectedBackground.rectTransform.anchoredPosition = new Vector2(0, 0);
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método SetMusicVolume, que establece el volumen de la música
    /// </summary>
    /// <param name="volume">Volumen</param>
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        GlobalVars.instance.musicVolume = musicVolume;
        foreach (Audio a in AudioManager.instance.music)
        {
            a.source.volume = musicVolume * a.volume;
        }
    }

    /// <summary>
    /// Método SetFXVolume, que establece el volumen de los sonidos
    /// </summary>
    /// <param name="volume">Volumen</param>
    public void SetFXVolume(float volume)
    {
        fxVolume = volume;
        GlobalVars.instance.fxVolume = fxVolume;
        foreach (Audio a in AudioManager.instance.sounds)
        {
            a.source.volume = fxVolume * a.volume;
        }
    }

    /// <summary>
    /// Método SetBrightness, que establece el nivel de brillo
    /// </summary>
    /// <param name="brightness"></param>
    public void SetBrightness(float brightness)
    {
        brightnessLvl = brightness;
        GlobalVars.instance.brightnessLvl = brightnessLvl;

        if (brightness > 0.5)
            brightnessImage.color = new Vector4(1.0f, 1.0f, 1.0f, brightness - 0.5f);
        else
            brightnessImage.color = new Vector4(0.0f, 0.0f, 0.0f, 0.5f - brightness);
    }

    /// <summary>
    /// Método SetLanguage, que cambia el idioma
    /// </summary>
    /// <param name="resolutionIndex"></param>
    public void SetLanguage(int languageIndex)
    {
        if (currentLanguageIndex != languageIndex)
        {
            LocalizationSystem.language = LocalizationSystem.GetLanguageByIndex(languageIndex);
            LocalizationTexts.instance.UpdateTexts();
            currentLanguageIndex = languageIndex;
            //languageSelectedBackground.rectTransform.localPosition = new Vector3(languageSelectedBackground.rectTransform.localPosition.x, backgroundPositions[currentLanguageIndex], 0);
            // Se mueven los puntos de anclaje (que son independientes del tamaño de la pantalla, van de 0 a 1) y se centra la imagen
            languageSelectedBackground.rectTransform.anchorMin = new Vector2(languageSelectedBackground.rectTransform.anchorMin.x, backgroundPositions[currentLanguageIndex][0]);
            languageSelectedBackground.rectTransform.anchorMax = new Vector2(languageSelectedBackground.rectTransform.anchorMax.x, backgroundPositions[currentLanguageIndex][1]);
            languageSelectedBackground.rectTransform.anchoredPosition = new Vector2(0,0);
            WriteOptions();
        }
    }

    /// <summary>
    /// Método HighlightOption, que destaca la opción que se esté modificando
    /// </summary>
    /// <param name="optionIndex">índice de la opción</param>
    public void HighlightOption(int optionIndex)
    {
        textImageGameObjects[optionIndex].SetActive(true);
        optionsTexts[optionIndex].color = highlightedColor;
    }

    /// <summary>
    /// Método PlayDownOptions, que pone de vuelta a la normalidad las opciones
    /// </summary>
    public void PlayDownOptions()
    {
        for (int i = 0; i < textImageGameObjects.Length; i++)
        {
            textImageGameObjects[i].SetActive(false);
            optionsTexts[i].color = normalColor;
        }
    }

    /// <summary>
    /// Método SaveOptions, que se llama para guardar las opciones del usuario
    /// </summary>
    public void SaveOptions()
    {
        WriteOptions();
    }

    /// <summary>
    /// Método WriteOptions, que guarda las opciones del usuario
    /// </summary>
    private void WriteOptions()
    {
        // PlayerPrefs
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("fxVolume", fxVolume);
        PlayerPrefs.SetFloat("brightnessLvl", brightnessLvl);
        PlayerPrefs.SetInt("languageIndex", currentLanguageIndex);

        PlayerPrefs.Save();
    }

    /// <summary>
    /// Método StartGameWithDifficulty, que empieza una nueva partida con la dificultad dada
    /// </summary>
    /// <param name="difficulty">Dificultad de la partida</param>
    public void StartGameWithDifficulty(int difficulty)
    {
        GlobalVars.instance.difficulty = difficulty;
        levelLoader.UseCircle(true);
        levelLoader.LoadScene(1);
    }
    #endregion
}
