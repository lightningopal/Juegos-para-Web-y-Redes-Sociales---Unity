using UnityEngine;

/// <summary>
/// Clase CameraRigMenu, que controla el funcionamiento de la cámara en el menú
/// </summary>
public class CameraRigMenu : MonoBehaviour
{
    #region Variables
    [Tooltip("Cantidad de rotación")]
    [SerializeField]
    private float rotationDelta = 4f;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        this.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        // Actualizar rotación
        this.transform.Rotate(new Vector3(0, 1, 0), rotationDelta * Time.deltaTime);
    }
    #endregion
}
