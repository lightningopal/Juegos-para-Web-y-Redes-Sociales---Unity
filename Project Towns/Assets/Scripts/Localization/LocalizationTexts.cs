using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase LocalizationTexts, que contiene a los textos localizados
/// </summary>
public class LocalizationTexts : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    public static LocalizationTexts instance;
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
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método UpdateTexts, que actualiza los textos localizados
    /// </summary>
    public void UpdateTexts()
    {
        if (!LocalizationSystem.isInit)
            LocalizationSystem.Init();

        LocalizedText[] localizedTexts = Resources.FindObjectsOfTypeAll<LocalizedText>();

        foreach (LocalizedText localizedText in localizedTexts)
        {
            localizedText.UpdateText();
        }

        // Si es un nivel
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            GameManager.instance.UpdateRobberiesTranslate();
        }

        // Si es el tutorial
        if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            TutorialGameManager.instance.UpdateRobberiesTranslate();
            //TutorialManager.instance.UpdateTutorialTranslate();
        }

        // Si es el menú principal
        if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            MenuManager.instance.UpdateStartTranslate();
        }
    }
    #endregion
}
