﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Clase CustomButton, que gestiona botones de forma personalizada
/// </summary>
public class CustomDifficultyButton : Selectable, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    #region Variables
    [Header("Referencias")]
    [Tooltip("Texto del botón")]
    [SerializeField]
    private TextMeshProUGUI buttonText = null;
    [Tooltip("Imagen del botón")]
    [SerializeField]
    private Image buttonImage = null;
    [Tooltip("Texto del tap again")]
    [SerializeField]
    private TextMeshProUGUI tapAgainText = null;

    [Header("Parámetros")]
    [Tooltip("Offset horizontal")]
    [SerializeField]
    private float horizontalOffset = 0f;
    [Tooltip("Offset vertical")]
    [SerializeField]
    private float verticalOffset = 0f;
    [Tooltip("Ángulo de rotación")]
    [SerializeField]
    private float rotationAngle = 0f;
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
    [Tooltip("Booleano que indica si está seleccionado")]
    [HideInInspector]
    public bool isSelected = false;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método OnPointerEnter, para cuando el ratón pasa sobre el botón
    /// </summary>
    /// <param name="eventData">Datos del puntero</param>
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        buttonText.color = highlightedColor;

        buttonImage.rectTransform.localPosition = new Vector3(buttonImage.rectTransform.localPosition.x + horizontalOffset, buttonImage.rectTransform.localPosition.y + verticalOffset, 0);
        buttonImage.rectTransform.Rotate(new Vector3(0, 0, 1), rotationAngle);

        buttonText.rectTransform.localPosition = new Vector3(buttonText.rectTransform.localPosition.x - horizontalOffset, buttonText.rectTransform.localPosition.y - verticalOffset, 0);
        buttonText.rectTransform.Rotate(new Vector3(0, 0, -1), rotationAngle);

        mouseOnButton = true;

        AudioManager.instance.PlaySound("OverButton");
    }

    /// <summary>
    /// Método OnPointerExit, para cuando el ratón sale del botón
    /// </summary>
    /// <param name="eventData">Datos del puntero</param>
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        if (!isClicked && !isSelected)
        {
            buttonText.color = normalColor;

            buttonImage.rectTransform.localPosition = new Vector3(buttonImage.rectTransform.localPosition.x - horizontalOffset, buttonImage.rectTransform.localPosition.y - verticalOffset, 0);
            buttonImage.rectTransform.Rotate(new Vector3(0, 0, -1), rotationAngle);

            buttonText.rectTransform.localPosition = new Vector3(buttonText.rectTransform.localPosition.x + horizontalOffset, buttonText.rectTransform.localPosition.y + verticalOffset, 0);
            buttonText.rectTransform.Rotate(new Vector3(0, 0, 1), rotationAngle);
        }
        mouseOnButton = false;
    }

    /// <summary>
    /// Método OnPointerDown, para cuando se hace click en el botón
    /// </summary>
    /// <param name="eventData">Datos del puntero</param>
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        buttonImage.color = new Color(buttonImage.color.r * clickAlpha, buttonImage.color.g * clickAlpha, buttonImage.color.b * clickAlpha);
        isClicked = true;
    }

    /// <summary>
    /// Método OnPointerUp, para cuando se deja de hacer click en el botón
    /// </summary>
    /// <param name="eventData">Datos del puntero</param>
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        buttonImage.color = new Color(buttonImage.color.r / clickAlpha, buttonImage.color.g / clickAlpha, buttonImage.color.b / clickAlpha);
        
        if (!mouseOnButton)
        {
            buttonText.color = normalColor;

            buttonImage.rectTransform.localPosition = new Vector3(buttonImage.rectTransform.localPosition.x - horizontalOffset, buttonImage.rectTransform.localPosition.y - verticalOffset, 0);
            buttonImage.rectTransform.Rotate(new Vector3(0, 0, -1), rotationAngle);

            buttonText.rectTransform.localPosition = new Vector3(buttonText.rectTransform.localPosition.x + horizontalOffset, buttonText.rectTransform.localPosition.y + verticalOffset, 0);
            buttonText.rectTransform.Rotate(new Vector3(0, 0, 1), rotationAngle);
        }
        isClicked = false;
    }

    /// <summary>
    /// Método OnDeselect, que se llama cuando se deja de seleccionar el objeto
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);

        if (Application.isMobilePlatform)
        {
            isSelected = false;
            tapAgainText.gameObject.SetActive(false);
        }

        buttonText.color = normalColor;

        buttonImage.rectTransform.localPosition = new Vector3(buttonImage.rectTransform.localPosition.x - horizontalOffset, buttonImage.rectTransform.localPosition.y - verticalOffset, 0);
        buttonImage.rectTransform.Rotate(new Vector3(0, 0, -1), rotationAngle);

        buttonText.rectTransform.localPosition = new Vector3(buttonText.rectTransform.localPosition.x + horizontalOffset, buttonText.rectTransform.localPosition.y + verticalOffset, 0);
        buttonText.rectTransform.Rotate(new Vector3(0, 0, 1), rotationAngle);
    }

    #endregion
}