using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    #region Variables
    [Tooltip("Lista de escenas")]
    [SerializeField]
    private List<string> scenes = new List<string>();
    private string sceneToLoad;

    [Tooltip("Animator")]
    [SerializeField]
    private Animator animator = null;
    #endregion

    #region Métodos Unity
    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        
    }
    #endregion

    #region Métodos Clase
    /// <summary>
    /// Método que recibe el id de la escena a cambiar, 
    /// dentro de la lista de escenas posibles.
    /// </summary>
    public void LoadScene(int id)
    {
        sceneToLoad = scenes[id];
        animator.SetTrigger("Start");
    }

    public void OnAnimationComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    #endregion
}
