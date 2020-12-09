using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [Tooltip("Player's Transform")]
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.position;   
    }
}
