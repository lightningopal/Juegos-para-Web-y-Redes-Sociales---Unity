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
    [Tooltip("Separación del borde")]
    [SerializeField]
    private float padding = 50.0f;

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        Vector3 convertedPosition = Camera.main.WorldToViewportPoint(robberyPosition);
        float dirX = Mathf.Clamp(convertedPosition.x, 0, 1);
        float dirY = Mathf.Clamp(convertedPosition.y, 0, 1);

        robberyRectTransform.localPosition = Camera.main.ViewportToScreenPoint(new Vector3(dirX, dirY, 0));

        // No funciona me agobio
        /*convertedPosition = Camera.main.ViewportToScreenPoint(convertedPosition);

        float dirX = Mathf.Clamp(convertedPosition.x, padding - (Screen.width / 2), (Screen.width / 2) - padding);
        float dirY = Mathf.Clamp(convertedPosition.y, padding - (Screen.height / 2), (Screen.height / 2) - padding);

        robberyRectTransform.localPosition = new Vector2(dirX, dirY);*/
    }
}
