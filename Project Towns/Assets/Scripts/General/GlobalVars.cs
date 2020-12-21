using UnityEngine;

/// <summary>
/// Clase GlobalVars, para almacenar información entre escenas
/// </summary>
public class GlobalVars : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    public static GlobalVars instance;

    public float musicVolume;
    public float fxVolume;
    public float brightnessLvl;

    public LocalizationSystem.Language language;
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
        musicVolume = 0.5f;
        fxVolume = 0.5f;

        foreach (Audio a in AudioManager.instance.music)
        {
            a.source.volume = musicVolume;
        }

        foreach (Audio a in AudioManager.instance.sounds)
        {
            a.source.volume = fxVolume;
        }

        brightnessLvl = 0f;

        LocalizationSystem.language = LocalizationSystem.Language.English;
        LocalizationTexts.instance.UpdateTexts();
    }
    #endregion
}
