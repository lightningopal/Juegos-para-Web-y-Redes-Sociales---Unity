using UnityEngine;
using System.IO;
using UnityEngine.Video;

/// <summary>
/// Clase VideoSceneManager, que controla la escena del vídeo del logo
/// </summary>
public class VideoSceneManager : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Referencia al level loader")]
    [SerializeField]
    private LevelLoader levelLoader = null;
    [Tooltip("Referencia al video player")]
    [SerializeField]
    private VideoPlayer videoPlayer = null;

    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        // Asignamos el delegado al evento
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "LogoAnimation.mp4");
        videoPlayer.Play();
        videoPlayer.loopPointReached += ChangeToMainMenu;
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        // Si el jugador hace click con el ratón
        if (Input.GetMouseButtonDown(0))
        {
            ChangeToMainMenu(videoPlayer);
        }
    }

    /// <summary>
    /// Método ChangeToMainMenu, que cambia al menú principal cuando acaba el vídeo
    /// </summary>
    /// <param name="vp">VideoPlayer vp</param>
    private void ChangeToMainMenu(VideoPlayer vp)
    {
        levelLoader.LoadScene(0);
        levelLoader.UseCircle(true);
    }

}
