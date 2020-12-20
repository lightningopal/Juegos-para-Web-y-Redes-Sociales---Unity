using UnityEngine;
using TMPro;

/// <summary>
/// Clase LocalizedText, que cambia el texto según la localización
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    #region Variables
    // Objeto de texto
    private TextMeshProUGUI textField;

    [Tooltip("Clave del texto para localización")]
    [SerializeField]
    private string key = null;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalizationSystem.GetLocalizedValue(key);
        textField.text = value;
    }
    #endregion
}
