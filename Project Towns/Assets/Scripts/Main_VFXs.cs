using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// El script simplemente accede al Rig de la cámara, y copia su rotación
/// </summary>
public class Main_VFXs : MonoBehaviour
{
    public CameraRig camRig;
    // Start is called before the first frame update
    void Start()
    {
        camRig = FindObjectOfType<CameraRig>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = camRig.transform.rotation;
    }
}
