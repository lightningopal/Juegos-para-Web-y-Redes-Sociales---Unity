using UnityEngine;

/// <summary>
/// El script simplemente accede al Rig de la cámara, y copia su rotación
/// </summary>
public class Main_VFXs : MonoBehaviour
{
    #region Variables
    [Tooltip("Camera Rig")]
    [SerializeField]
    private CameraRig camRig;
    #endregion

    #region Metodos Unity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        camRig = FindObjectOfType<CameraRig>();
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        this.transform.rotation = camRig.transform.rotation;
    }
    #endregion

}
