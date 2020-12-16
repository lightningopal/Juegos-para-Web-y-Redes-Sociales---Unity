using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    [HideInInspector]
    public static SceneChanger instance;

    [Tooltip("Animator")]
    [SerializeField]
    private Animator animator = null;

    // Índice de la escena a cargar
    private int SceneToLoad;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Awake, que se ejecuta al cargar el script
    /// </summary>
    private void Awake()
    {
        // Se instancia a si misma
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método FadeToScene, que hace un fade hacia la escena dada
    /// </summary>
    /// <param name="SceneIndex">Índice de la escena a ir</param>
    public void FadeToScene(int SceneIndex)
    {
        SceneToLoad = SceneIndex;
        animator.SetTrigger("FadeOut");
    }

    /// <summary>
    /// Método FadeToNextScene, que hace un fade hacia la siguiente escena
    /// </summary>
    public void FadeToNextScene()
    {
        FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Método ResetScene, que hace un fade y resetea la escena
    /// </summary>
    public void ResetScene()
    {
        SceneToLoad = SceneManager.GetActiveScene().buildIndex;
        animator.SetTrigger("FadeOut");
    }

    /// <summary>
    /// Método OnFadeComplete, que se llama cuando se completa el fade
    /// </summary>
    public void OnFadeComplete()
    {
        if (SceneToLoad == -1)
            Application.Quit();
        else
            SceneManager.LoadScene(SceneToLoad);
    }
    #endregion
}