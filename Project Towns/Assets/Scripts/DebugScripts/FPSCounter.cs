using TMPro;
using UnityEngine;

/// <summary>
/// Clase FPSCounter, para llevar el control del rendimiento del juego
/// </summary>
public class FPSCounter : MonoBehaviour
{
    // Variables de control
    [Tooltip("Texto de los FPS")]
    public TextMeshProUGUI FPSCounterText;

    private const float TIME_FOR_UPDATE = 0.25f;
    private float nextFPSUpdate = 0.0f;

    private int framesCount = 0;
    private float framesTimesSum = 0f;

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        // Suma uno al numero de frames y suma el tiempo que ha tardado en procesarse el anterior
        framesCount++;
        framesTimesSum += Time.deltaTime;

        // Si ha pasado el tiempo suficiente para una actualización del contador
        if (Time.time > nextFPSUpdate)
        {
            // Prepara la próxima actualización
            nextFPSUpdate = Time.time + TIME_FOR_UPDATE;

            // Calcula y asigna el número de frames por segundo
            int fps = Mathf.RoundToInt(1 / (framesTimesSum / framesCount));
            FPSCounterText.text = "FPS: " + fps;

            // Resetea los contadores
            framesCount = 0;
            framesTimesSum = 0;
        }
    }
}
