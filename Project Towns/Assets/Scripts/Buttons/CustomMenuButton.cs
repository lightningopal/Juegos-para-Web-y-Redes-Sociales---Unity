using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Clase CustomMenuButton, que gestiona botones de forma personalizada
/// </summary>
public class CustomMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    #region Variables
    [Header("Referencias")]
    [Tooltip("Texto del botón")]
    [SerializeField]
    private TextMeshProUGUI buttonText = null;
    [Tooltip("Imagen del botón")]
    [SerializeField]
    private Image buttonImage = null;
    [Tooltip("GameObject de la segunda imagen del botón")]
    [SerializeField]
    private GameObject secondImage = null;

    [Header("Parámetros")]
    [Tooltip("Offset horizontal")]
    [SerializeField]
    private float horizontalOffset = 15f;
    [Tooltip("Offset vertical")]
    [SerializeField]
    private float verticalOffset = 0f;
    [Tooltip("Ángulo de rotación")]
    [SerializeField]
    private float rotationAngle = 2.0f;
    [Tooltip("Ángulo de rotación del texto")]
    [SerializeField]
    private float rotationTextAngle = 2.0f;
    [Tooltip("Alpha al hacer click")]
    [SerializeField]
    private float clickAlpha = 0.8f;

    [Header("Colores")]
    [Tooltip("Color del texto normal")]
    [SerializeField]
    private Color normalColor = new Color();
    [Tooltip("Color del texto seleccionado")]
    [SerializeField]
    private Color highlightedColor = new Color();

    // Variables de control
    private bool isClicked = false;
    private bool mouseOnButton = false;

    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método OnPointerEnter, para cuando el ratón pasa sobre el botón
    /// </summary>
    /// <param name="eventData">Datos del puntero</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = highlightedColor;

        buttonImage.rectTransform.localPosition = new Vector3(buttonImage.rectTransform.localPosition.x + horizontalOffset, buttonImage.rectTransform.localPosition.y + verticalOffset, 0);
        buttonImage.rectTransform.Rotate(new Vector3(0, 0, 1), rotationAngle);

        buttonText.rectTransform.localPosition = new Vector3(buttonText.rectTransform.localPosition.x - horizontalOffset, buttonText.rectTransform.localPosition.y - verticalOffset, 0);
        buttonText.rectTransform.Rotate(new Vector3(0, 0, -1), rotationAngle);
        buttonText.rectTransform.Rotate(new Vector3(0, 0, 1), rotationTextAngle);

        mouseOnButton = true;

        secondImage.SetActive(true);

        AudioManager.instance.PlaySound("OverButton");
    }

    /// <summary>
    /// Método OnPointerExit, para cuando el ratón sale del botón
    /// </summary>
    /// <param name="eventData">Datos del puntero</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked)
        {
            buttonText.color = normalColor;

            buttonImage.rectTransform.localPosition = new Vector3(buttonImage.rectTransform.localPosition.x - horizontalOffset, buttonImage.rectTransform.localPosition.y - verticalOffset, 0);
            buttonImage.rectTransform.Rotate(new Vector3(0, 0, -1), rotationAngle);

            buttonText.rectTransform.localPosition = new Vector3(buttonText.rectTransform.localPosition.x + horizontalOffset, buttonText.rectTransform.localPosition.y + verticalOffset, 0);
            buttonText.rectTransform.Rotate(new Vector3(0, 0, 1), rotationAngle);
            buttonText.rectTransform.Rotate(new Vector3(0, 0, -1), rotationTextAngle);

            secondImage.SetActive(false);
        }
        mouseOnButton = false;
    }

    /// <summary>
    /// Método OnPointerDown, para cuando se hace click en el botón
    /// </summary>
    /// <param name="eventData">Datos del puntero</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.color = new Color(buttonImage.color.r * clickAlpha, buttonImage.color.g * clickAlpha, buttonImage.color.b * clickAlpha);
        isClicked = true;
    }

    /// <summary>
    /// Método OnPointerUp, para cuando se deja de hacer click en el botón
    /// </summary>
    /// <param name="eventData">Datos del puntero</param>
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.color = new Color(buttonImage.color.r / clickAlpha, buttonImage.color.g / clickAlpha, buttonImage.color.b / clickAlpha);

        if (!mouseOnButton)
        {
            buttonText.color = normalColor;

            buttonImage.rectTransform.localPosition = new Vector3(buttonImage.rectTransform.localPosition.x - horizontalOffset, buttonImage.rectTransform.localPosition.y - verticalOffset, 0);
            buttonImage.rectTransform.Rotate(new Vector3(0, 0, -1), rotationAngle);

            buttonText.rectTransform.localPosition = new Vector3(buttonText.rectTransform.localPosition.x + horizontalOffset, buttonText.rectTransform.localPosition.y + verticalOffset, 0);
            buttonText.rectTransform.Rotate(new Vector3(0, 0, 1), rotationAngle);
            buttonText.rectTransform.Rotate(new Vector3(0, 0, 1), -rotationTextAngle);

            secondImage.SetActive(false);
        }
        isClicked = false;
    }
    #endregion
}
