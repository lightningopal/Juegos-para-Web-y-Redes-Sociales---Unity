using UnityEngine;
using UnityEngine.UI;

public class Robbery : MonoBehaviour
{
    [Tooltip("Posición donde se produjo el robo")]
    [HideInInspector]
    public Vector3 robberyPosition;
    [Tooltip("Imagen del icono de robo")]
    public Image robberyIconImage;
}
