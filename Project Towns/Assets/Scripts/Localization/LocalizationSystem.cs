using System.Collections.Generic;

/// <summary>
/// Clase LocalizationSystem, que controla los idiomas del juego
/// </summary>
public class LocalizationSystem
{
    /// <summary>
    /// Enumerador para el idioma
    /// </summary>
    public enum Language
    {
        English,
        Spanish
    }

    #region Variables
    // Idioma del juego
    public static Language language = Language.English;

    // Diccionarios de idioma
    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedES;

    // Booleano que indica si se ha iniciadoel sistema de localización
    public static bool isInit;
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método Init, que inicializa el Sistema de Localización
    /// </summary>
    public static void Init()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        localisedEN = csvLoader.GetDictionaryValues("en");
        localisedES = csvLoader.GetDictionaryValues("es");

        isInit = true;
    }

    /// <summary>
    /// Método GetLocalizedValue, que dada una clave devuelve su traducción
    /// </summary>
    /// <param name="key">Clave</param>
    /// <returns>String con la traducción</returns>
    public static string GetLocalizedValue(string key)
    {
        if (!isInit)
            Init();

        string value = key;

        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Spanish:
                localisedES.TryGetValue(key, out value);
                break;
        }

        return value;
    }
    #endregion
}
