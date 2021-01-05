using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase CustomDetentionButton, para el botón de detención
/// </summary>
public class CustomDetentionButton : MonoBehaviour
{
    [Tooltip("Transform del target")]
    [HideInInspector]
    public Transform targetTransform;

    [Tooltip("Padding")]
    public float padding = 50.0f;
    [Tooltip("Propio RectTransform")]
    [SerializeField]
    private RectTransform rectTransform = null;
    [Tooltip("Canvas RT")]
    [SerializeField]
    private RectTransform canvasRT = null;
    [Tooltip("Cámara principal")]
    private Camera mainCamera;

    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(targetTransform.position);

            float pointX = Mathf.Clamp(screenPoint.x, -canvasRT.sizeDelta.x / 2 + padding, canvasRT.sizeDelta.x / 2 - padding);
            float pointY = Mathf.Clamp(screenPoint.y, -canvasRT.sizeDelta.y / 2 + padding, canvasRT.sizeDelta.y / 2 - padding);

            rectTransform.localPosition = new Vector3(pointX,pointY, 0);
        }
    }
}
