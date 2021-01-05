using UnityEngine;
using UnityEngine.UI;

public class Robbery : MonoBehaviour
{
    [Tooltip("Posición donde se produjo el robo")]
    [HideInInspector]
    public Vector3 robberyPosition;
    [Tooltip("Imagen del icono de robo")]
    public Image robberyIconImage;
    [Tooltip("Rect Transform del icono de robo")]
    public RectTransform robberyRectTransform;
    [Tooltip("Transform del jugador")]
    private Transform playerTransform;
    [Tooltip("Referencia a la cámara")]
    private Camera mainCamera = null;
    private RectTransform canvasRT;
    [Tooltip("Separación del borde")]
    [SerializeField]
    private float padding = 60.0f;

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        mainCamera = Camera.main;
        canvasRT = this.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(robberyPosition);

        // Se pasa de (0,0) - (1,1) a (-ancho/2, -alto/2) - (ancho/2, alto/2)
        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((viewportPosition.x * canvasRT.sizeDelta.x) - (canvasRT.sizeDelta.x * 0.5f)),
            ((viewportPosition.y * canvasRT.sizeDelta.y) - (canvasRT.sizeDelta.y * 0.5f))
         );
        // Si el punto está detrás de la cámara, se invierten las componentes X e Y
        if (viewportPosition.z < 0)
        {
            WorldObject_ScreenPosition.x = -WorldObject_ScreenPosition.x;
            WorldObject_ScreenPosition.y = -WorldObject_ScreenPosition.y;
        }
        
        // Se ajusta la imagen entre el mínimo/máximo del ancho y alto del canvas
        float dirX = Mathf.Clamp(WorldObject_ScreenPosition.x, -canvasRT.sizeDelta.x/2 + padding, canvasRT.sizeDelta.x/2 - padding);
        float dirY = Mathf.Clamp(WorldObject_ScreenPosition.y, -canvasRT.sizeDelta.y/2 + padding, canvasRT.sizeDelta.y/2 - padding);

        robberyRectTransform.anchoredPosition = new Vector2(dirX, dirY);

        // ROTACIÓN //
        // Se pasa de (-ancho/2, -alto/2) - (ancho/2, alto/2) a (-1,-1) - (1,1)
        float rotX = dirX / (canvasRT.sizeDelta.x / 2 - padding);
        float rotY = dirY / (canvasRT.sizeDelta.y / 2 - padding);
        // Se calcula el ángulo de rotación
        Vector3 normalizedRot = new Vector3(rotX, rotY, 0.0f).normalized;
        float angle = Mathf.Atan2(normalizedRot.x, normalizedRot.y) * Mathf.Rad2Deg;
        robberyRectTransform.localRotation = Quaternion.AngleAxis(angle+180, -Vector3.forward);

    }
}
