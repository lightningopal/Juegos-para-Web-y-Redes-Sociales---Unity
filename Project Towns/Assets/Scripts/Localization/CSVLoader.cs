using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    // Array de strings de separadores de campo
    private readonly string[] fieldSeparator = { ";" };
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

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = line.Split(';');

            if (fields.Length > attributeIndex)
            {
                var key = fields[0];

                if (dictionary.ContainsKey(key))
                    continue;

                fields[attributeIndex] = fields[attributeIndex].TrimEnd('\r', '\n');

                var value = (fields[attributeIndex] != string.Empty) ? fields[attributeIndex] : key;

                dictionary.Add(key, value);
            }
        }

        return dictionary;
    }

#if UNITY_EDITOR
    /// <summary>
    /// Método Add, para añadir una nueva clave
    /// </summary>
    /// <param name="key">Clave</param>
    /// <param name="value">Valor en inglés</param>
    public void Add(string key, string value)
    {
        string appended = string.Format("\n{0};{1};", key, value);
        File.AppendAllText("Assets/Resources/Localization.csv", appended);

        UnityEditor.AssetDatabase.Refresh();
    }

    /// <summary>
    /// Método Remove, para borrar una clave
    /// </summary>
    /// <param name="key">Clave a borrar</param>
    public void Remove(string key)
    {
        string[] lines = csvFile.text.Split(lineSeparator);

        string[] keys = new string[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            keys[i] = line.Split(fieldSeparator, System.StringSplitOptions.None)[0];
        }

        int index = -1;

        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].Contains(key))
            {
                index = i;
                break;
            }
        }

        if (index > -1)
        {
            string[] newLines;
            newLines = lines.Where(w => w != lines[index]).ToArray();

            string replaced = string.Join(lineSeparator.ToString(), newLines);
            File.WriteAllText("Assets/Resources/Localization.csv", replaced);
        }
    }

    /// <summary>
    /// Método Edit, para editar una clave
    /// </summary>
    /// <param name="key">Clave a editar</param>
    /// <param name="value">Valor</param>
    public void Edit(string key, string value)
    {
        Remove(key);
        Add(key, value);
    }
#endif
    #endregion
}
