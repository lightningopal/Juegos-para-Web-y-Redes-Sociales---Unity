using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    #region Variables

    private bool changeScene = false;

    AsyncOperation loader;

    [Tooltip("Círculo de transición")]
    [SerializeField]
    private GameObject circle = null;
    [SerializeField]
    private bool useCircle = false;

    [Tooltip("Barras UI")]
    [SerializeField]
    private GameObject bars = null;
    private bool showBars = false;

    [Tooltip("Número de capas de animación")]
    [SerializeField]
    private int numLayers = 0;

    [Tooltip("Lista de escenas")]
    [SerializeField]
    private List<string> scenes = new List<string>();
    private string sceneToLoad;

    [Tooltip("Lista de Canvas")]
    [SerializeField]
    private List<GameObject> UICanvas = new List<GameObject>();
    private GameObject canvasToLoad;
    private GameObject currentCanvas;

    [Tooltip("Animator")]
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private List<RuntimeAnimatorController> animators = new List<RuntimeAnimatorController>();
    #endregion

    #region Métodos Unity
    private void Start()
    {
        if (UICanvas.Capacity != 0)
        {
            currentCanvas = UICanvas[0];
        }
    }
    #endregion

    #region Métodos Clase
    /// <summary>
    /// Cambia el animador en tiempo de ejecución, para poder
    /// realizar distintas transiciones en una misma escena
    /// (De juego a pause y viceversa, por ejemplo)
    /// </summary>
    /// <param name="id"></param>
    public void ChangeAnimator(int id)
    {
        animator.runtimeAnimatorController = animators[id];
    }

    /// <summary>
    /// Método que recibe el id de la escena a cambiar, 
    /// dentro de la lista de escenas posibles.
    /// </summary>
    /// <param name="id"></param>
    public void LoadScene(int id)
    {
        sceneToLoad = scenes[id];
        //loader = SceneManager.LoadSceneAsync(sceneToLoad);
        //loader.allowSceneActivation = false;
        animator.SetTrigger("Leave");
        changeScene = true;
    }

    /// <summary>
    /// Al terminar la transición, se realiza el cambio de escena
    /// </summary>
    public void ChangeScene()
    {
        if (changeScene)
        {
            loader = SceneManager.LoadSceneAsync(sceneToLoad);
            //loader.allowSceneActivation = true;
            //SceneManager.LoadScene(sceneToLoad);
        }
    }

    /// <summary>
    /// Activa o desactiva el círculo para las transiciones
    /// </summary>
    /// <param name="useCircle"></param>
    public void UseCircle(bool useCircle)
    {
        this.useCircle = useCircle;
        if (this.useCircle)
        {
            for (int i = 0; i < numLayers; i++)
            {
                animator.SetLayerWeight(i, 0);
            }

            circle.SetActive(true);
        }
        else
        {
            for (int i = 0; i < numLayers; i++)
            {
                animator.SetLayerWeight(i, 1);
            }
            circle.SetActive(false);
        }
    }
    /// <summary>
    /// Método que recibe el id del canvas a cambiar,
    /// dentro de la lista de canvas posibles.
    /// </summary>
    /// <param name="id"></param>
    public void LoadCanvas(int id)
    {
        canvasToLoad = UICanvas[id];
        animator.SetTrigger("Leave");
        changeScene = false;
    }

    /// <summary>
    /// Al terminar la transición, se cambia el canvas actual por el nuevo
    /// </summary>
    public void ChangeCanvas()
    {
        if (!changeScene)
        {
            currentCanvas.SetActive(false);
            currentCanvas = canvasToLoad;
            canvasToLoad.SetActive(true);
            animator.SetTrigger("Enter");
        }
    }

    /// <summary>
    /// Determina si hay que mostrar
    /// o esconder las barras
    /// </summary>
    /// <param name="showBars"></param>
    public void ShowBars(bool showBars)
    {
        this.showBars = showBars;
    }

    /// <summary>
    /// Activa o desactiva las barras de la interfaz
    /// Se llama tras la animación del círculo
    /// </summary>
    public void CheckBars()
    {
        if (showBars)
        {
            bars.SetActive(true);
        }else
        {
            bars.SetActive(false);
        }
    }
    #endregion
}