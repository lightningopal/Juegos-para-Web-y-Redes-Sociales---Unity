using UnityEngine;

/// <summary>
/// Clase CameraRig, que controla el funcionamiento de la cámara
/// </summary>
public class CameraRig : MonoBehaviour
{
    [Tooltip("Player's Transform")]
    public Transform player;

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        this.transform.position = player.position;   
    }

    /// <summary>
    /// Método RotateHorary, para girar la cámara en sentido horario
    /// </summary>
    public void RotateHorary()
    {
        this.transform.Rotate(new Vector3(0, 90, 0), Space.World);
    }

    /// <summary>
    /// Método RotateAntihorary, para girar la cámara en sentido antihorario
    /// </summary>
    public void RotateAntihorary()
    {
        this.transform.Rotate(new Vector3(0, -90, 0), Space.World);
    }
}
