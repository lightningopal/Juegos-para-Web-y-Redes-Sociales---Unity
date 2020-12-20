using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Clase CSVLoader, que lee datos desde un CSV
/// </summary>
public class CSVLoader
{
    #region Variables
    // Archivo CSV
    private TextAsset csvFile;

    // Separador de línea
    private readonly char lineSeparator = '\n';

    // Carácter que envuelve
    private readonly char surround = '"';

    // Array de strings de separadores de campo
    private readonly string[] fieldSeparator = { "\",\"" };
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método LoadCSV, que carga el archivo desde Resources
    /// </summary>
    public void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("Localization");
    }

    /// <summary>
    /// Método GetDictionaryValues, que obtiene los valores del CSV y los introduce al diccionario
    /// </summary>
    /// <param name="attributeID">ID de Idioma</param>
    /// <returns>Diccionario con las traducciones</returns>
    public Dictionary<string, string> GetDictionaryValues(string attributeID)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string[] lines = csvFile.text.Split(lineSeparator);
        int attributeIndex = -1;
        string[] headers = lines[0].Split(fieldSeparator, System.StringSplitOptions.None);

        for (int i = 0; i < headers.Length; i++)
        {
            if (headers[i].Contains(attributeID))
            {
                attributeIndex = i;
                break;
            }
        }

        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = CSVParser.Split(line);

            for (int j = 0; j < fields.Length; j++)
            {
                fields[j] = fields[j].TrimStart(' ', surround);
                fields[j] = fields[j].TrimEnd('\r', surround);
            }

            if (fields.Length > attributeIndex)
            {
                var key = fields[0];

                if (dictionary.ContainsKey(key))
                    continue;

                var value = fields[attributeIndex];

                dictionary.Add(key, value);
            }
        }

        return dictionary;
    }
    #endregion
}
