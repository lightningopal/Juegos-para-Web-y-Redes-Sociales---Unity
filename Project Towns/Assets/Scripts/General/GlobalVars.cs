using UnityEngine;

/// <summary>
/// Clase GlobalVars, para almacenar información entre escenas
/// </summary>
public class GlobalVars : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    public static GlobalVars instance;

    [Header("Opciones")]
    [Tooltip("Volumen de la música")]
    public float musicVolume;
    [Tooltip("Volumen de los efectos de sonido")]
    public float fxVolume;
    [Tooltip("Nivel de brillo")]
    public float brightnessLvl;
    [Tooltip("Idioma")]
    public LocalizationSystem.Language language;

    [Header("Juego")]
    [Tooltip("Dificultad")]
    public int difficulty;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Awake, que se llama al cargar el script
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        ReadOptions();
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método ReadOptions, que lee las opciones guardadas del jugador
    /// </summary>
    private void ReadOptions()
    {
        // PlayerPrefs
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        fxVolume = PlayerPrefs.GetFloat("fxVolume", 0.5f);
        brightnessLvl =  PlayerPrefs.GetFloat("brightnessLvl", 0f);
        int languageIndex = PlayerPrefs.GetInt("languageIndex", 0);

        foreach (Audio a in AudioManager.instance.music)
        {
            a.source.volume = musicVolume;
        }

        foreach (Audio a in AudioManager.instance.sounds)
        {
            a.source.volume = fxVolume;
        }

        LocalizationSystem.language = LocalizationSystem.GetLanguageByIndex(languageIndex);
        LocalizationTexts.instance.UpdateTexts();
    }
    #endregion
}
