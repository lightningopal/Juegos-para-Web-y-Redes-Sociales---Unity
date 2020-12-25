using System.Collections;
using System.Collections.Generic;
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
    // Método Start, que se llama al iniciar el objeto
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
