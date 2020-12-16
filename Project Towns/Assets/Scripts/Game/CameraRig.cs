using UnityEngine;

/// <summary>
/// Clase CameraRig, que controla el funcionamiento de la cámara
/// </summary>
public class CameraRig : MonoBehaviour
{
    #region Variables
    [Tooltip("Player's Transform")]
    [SerializeField]
    private Transform player = null;

    [Tooltip("Lerp Pass")]
    [SerializeField]
    private float lerpPass = 4f;

    // Rotation to go
    private float desiredRotationY = 0;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        // Seguir al jugador
        this.transform.position = player.position;

        // Actualizar rotación (si fuera necesario)
        if (Mathf.Abs(Mathf.Abs(this.transform.rotation.eulerAngles.y) - Mathf.Abs(desiredRotationY)) > (float.Epsilon * lerpPass))
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(this.transform.rotation.eulerAngles.x, desiredRotationY, this.transform.rotation.eulerAngles.z));

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                targetRotation, lerpPass * Time.deltaTime);
        }
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método RotateHorary, para girar la cámara en sentido horario
    /// </summary>
    public void RotateHorary()
    {
        desiredRotationY += 90;
        if (desiredRotationY > 271)
            desiredRotationY = 0;
    }

    /// <summary>
    /// Método RotateAntihorary, para girar la cámara en sentido antihorario
    /// </summary>
    public void RotateAntihorary()
    {
        desiredRotationY -= 90;
        if (desiredRotationY < -1)
            desiredRotationY = 270;
    }
    #endregion
}
