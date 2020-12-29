using UnityEngine;

/// <summary>
/// Clase MenuManager, que controla el menú del juego
/// </summary>
public class MenuManager : MonoBehaviour
{
    #region Variables
    [Tooltip("LevelLoader")]
    [SerializeField]
    private LevelLoader levelLoader = null;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        foreach (Audio a in AudioManager.instance.music)
        {
            AudioManager.instance.ManageAudio(a.name, "music", "stop");
            AudioManager.instance.ManageAudio(a.name, "music", "unpause");
        }
        //AudioManager.instance.ManageAudio("MainTheme", "music", "play");
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método StartGameWithDifficulty, que empieza una nueva partida con la dificultad dada
    /// </summary>
    /// <param name="difficulty">Dificultad de la partida</param>
    public void StartGameWithDifficulty(int difficulty)
    {
        GlobalVars.instance.difficulty = difficulty;
        levelLoader.LoadScene(0);
    }
    #endregion
}
