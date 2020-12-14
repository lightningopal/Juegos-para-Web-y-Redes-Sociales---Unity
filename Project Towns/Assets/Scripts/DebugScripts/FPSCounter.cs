using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI FPSCounterText;
    private const float TIME_FOR_UPDATE = 0.25f;
    private float nextFPSUpdate = 0.0f;
    private int framesCount = 0;
    private float framesTimesSum = 0f;

    // Update is called once per frame
    void Update()
    {
        framesCount++;
        framesTimesSum += Time.deltaTime;
        if (Time.time > nextFPSUpdate)
        {
            nextFPSUpdate = Time.time + TIME_FOR_UPDATE;
            int fps = Mathf.RoundToInt(1 / (framesTimesSum / framesCount));
            FPSCounterText.text = "FPS: " + fps;
            framesCount = 0;
            framesTimesSum = 0;
        }
    }
}
