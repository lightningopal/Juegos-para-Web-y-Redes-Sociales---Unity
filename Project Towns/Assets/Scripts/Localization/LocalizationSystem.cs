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
        English = 0,
        Spanish = 1
    }

    #region Variables
    // Idioma del juego
    public static Language language;

    // Diccionarios de idioma
    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedES;

    // Instancia estática del CSVLoader
    public static CSVLoader csvLoader;

    // Booleano que indica si se ha iniciadoel sistema de localización
    public static bool isInit;
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método Init, que inicializa el Sistema de Localización
    /// </summary>
    public static void Init()
    {
        csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        UpdateDictionaries();

        isInit = true;
    }

    /// <summary>
    /// Método UpdateDictionaries, que actualiza los diccionarios
    /// </summary>
    public static void UpdateDictionaries()
    {
        localisedEN = csvLoader.GetDictionaryValues("en");
        localisedES = csvLoader.GetDictionaryValues("es");
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

    /// <summary>
    /// Método GetLocalizedValue, que dada una clave e índice devuelve su traducción
    /// </summary>
    /// <param name="key">Clave</param>
    /// <param name="index">Índice</param>
    /// <returns>String con la traducción</returns>
    public static string GetLocalizedValue(string key, int index)
    {
        if (!isInit)
            Init();

        string value = key;

        switch (index)
        {
            case 0:
                localisedEN.TryGetValue(key, out value);
                break;
            case 1:
                localisedES.TryGetValue(key, out value);
                break;
        }

        return value;
    }

    public static Language GetLanguageByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return Language.English;
            case 1:
                return Language.Spanish;
            default:
                return Language.English;
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Método Add, para añadir una nueva clave
    /// </summary>
    /// <param name="key">Clave</param>
    /// <param name="value">Valor en inglés</param>
    public static void Add(string key, string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Add(key, value);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }

    /// <summary>
    /// Método Remove, para borrar una clave
    /// </summary>
    /// <param name="key">Clave a borrar</param>
    public static void Replace(string key, string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Edit(key, value);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }

    /// <summary>
    /// Método Edit, para editar una clave
    /// </summary>
    /// <param name="key">Clave a editar</param>
    /// <param name="value">Valor</param>
    public static void Remove(string key)
    {
        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Remove(key);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }

    /// <summary>
    /// Método GetDictionaryForEditor, que devuelve el diccionario inglés para la GUI
    /// </summary>
    /// <returns>Diccionario inglés</returns>
    public static Dictionary<string, string> GetDictionaryForEditor()
    {
        if (!isInit)
            Init();

        return localisedEN;
    }
#endif
#endregion
}
