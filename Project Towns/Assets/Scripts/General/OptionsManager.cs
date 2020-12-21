using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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
    [Tooltip("URP PostProcess Volume")]
    [SerializeField]
    private Volume postProcessVolume = null;

    #region VolumeComponents
    // Ajustes de color
    private ColorAdjustments colorAdjustments;
    #endregion

    [Tooltip("Slider brillo")]
    [SerializeField]
    private Slider brightnessSlider = null;

    // Nivel brillo
    private float brightnessLvl;

    // Idioma
    [Tooltip("Dropdown de idiomas")]
    [SerializeField]
    private TMP_Dropdown languagesDropdown = null;
    private int currentLanguageIndex;

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
        brightnessSlider.value = brightnessLvl;

        // Obtener componentes de post proceso
        postProcessVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        colorAdjustments.postExposure.value = brightnessLvl;

        // Idioma
        currentLanguageIndex = (int)LocalizationSystem.language;
        languagesDropdown.value = currentLanguageIndex;
        languagesDropdown.RefreshShownValue();
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
            a.source.volume = musicVolume;
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
            a.source.volume = fxVolume;
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

        colorAdjustments.postExposure.value = brightnessLvl;
    }

    /// <summary>
    /// Método SetLanguage, que cambia el idioma
    /// </summary>
    /// <param name="resolutionIndex"></param>
    public void SetLanguage(int languageIndex)
    {
        LocalizationSystem.language = LocalizationSystem.GetLanguageByIndex(languageIndex);
        LocalizationTexts.instance.UpdateTexts();
        WriteOptions();
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
        /*//Path the our text file.
        string path = "./Dance Kingdom_Data/Resources/options.txt";

        //Erase all content of the text file.
        System.IO.File.WriteAllText(path, string.Empty);

        //Writer to write on our text file.
        StreamWriter writer = new StreamWriter(path, true);

        //We write our options in the text file.
        writer.WriteLine("musicVolume:" + musicVolume);
        writer.WriteLine("fxVolume:" + fxVolume);
        writer.WriteLine("brightnessLvl:" + brightnessLvl);
        
        //Close the writer.
        writer.Close();*/
    }
    #endregion
}
