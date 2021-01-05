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
    public float padding = 10.0f;
    private float offsetY;
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
        offsetY = canvasRT.sizeDelta.y / padding;
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            // (0,0)-(1,1)
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(targetTransform.position);

            Vector2 screenPoint = new Vector2((viewportPosition.x - 0.5f) * canvasRT.sizeDelta.x, (viewportPosition.y - 0.5f) * canvasRT.sizeDelta.y);

            rectTransform.localPosition = new Vector3(screenPoint.x, screenPoint.y + offsetY, 0);
        }
    }
}
