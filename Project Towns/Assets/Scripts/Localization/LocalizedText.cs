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
    private LocalizedString localizedString = null;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Awake, que se ejecuta cuando carga el script
    /// </summary>
    void Awake()
    {
        textField = GetComponent<TextMeshProUGUI>();
        string value = localizedString.value;
        textField.text = value;
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método UpdateText, que actualiza el valor del texto
    /// </summary>
    public void UpdateText()
    {
        textField = GetComponent<TextMeshProUGUI>();
        string value = localizedString.value;
        textField.text = value;
    }

    /// <summary>
    /// Método ChangeText, permite cambiar el valor del texto
    /// </summary>
    public void ChangeText(string key)
    {
        localizedString = key;
        textField = GetComponent<TextMeshProUGUI>();
        string value = localizedString.value;
        textField.text = value;
    }
    #endregion
}
