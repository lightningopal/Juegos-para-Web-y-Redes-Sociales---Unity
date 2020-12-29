using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Clase CustomBackButton, que gestiona el botón de ir hacia atrás
/// </summary>
public class CustomBackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Variables
    [Header("Referencias")]
    [Tooltip("Imagen del botón")]
    [SerializeField]
    private Image buttonImage = null;

    [Header("Parámetros")]
    [Tooltip("Alpha al hacer click")]
    [SerializeField]
    private float clickAlpha = 0.8f;

    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método OnPointerDown, para cuando se hace click en el botón
    /// </summary>
    /// <param name="eventData">Datos del puntero</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.color = new Color(buttonImage.color.r * clickAlpha, buttonImage.color.g * clickAlpha, buttonImage.color.b * clickAlpha);
    }

    /// <summary>
    /// Método OnPointerUp, para cuando se deja de hacer click en el botón
    /// </summary>
    /// <param name="eventData">Datos del puntero</param>
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.color = new Color(buttonImage.color.r / clickAlpha, buttonImage.color.g / clickAlpha, buttonImage.color.b / clickAlpha);
    }
    #endregion
}
