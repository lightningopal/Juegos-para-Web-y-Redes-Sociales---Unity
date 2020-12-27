﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    #region Variables

    private bool changeScene = false;

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
        //canvasToLoad = currentCanvas;
        //animator.SetTrigger("Start");
        //changeScene = false;
    }

    /// <summary>
    /// Método que recibe el id de la escena a cambiar, 
    /// dentro de la lista de escenas posibles.
    /// </summary>
    /// <param name="id"></param>
    public void LoadScene(int id)
    {
        sceneToLoad = scenes[id];
        animator.SetTrigger("Start");
        changeScene = true;
    }

    /// <summary>
    /// Al terminar la transición, se realiza el cambio de escena
    /// </summary>
    public void ChangeScene()
    {
        if (changeScene)
        {
            SceneManager.LoadScene(sceneToLoad);
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
        animator.SetTrigger("Start");
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
            animator.SetTrigger("Restart");
        }
    }
    #endregion
}