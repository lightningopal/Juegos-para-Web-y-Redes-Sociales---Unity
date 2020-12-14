using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI FPSCounterText;
    private float nextFPSUpdate = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFPSUpdate)
        {
            nextFPSUpdate = Time.time + 1.0f;
            int fps = Mathf.RoundToInt(1 / Time.deltaTime);
            FPSCounterText.text = "FPS: " + fps;
        }
    }
}
