﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    public static UIManager instance;

    [Header("Textos")]
    [Tooltip("Texto que cuenta los robos")]
    [SerializeField]
    private TextMeshProUGUI robberiesText = null;

    [Header("Background Corazones")]
    [Tooltip("RectTransform del fondo de los intentos")]
    [SerializeField]
    private RectTransform attemptsBackground = null;
    [Tooltip("Lista de tamaños del background")]
    [SerializeField]
    private float[] backgroundWidths = new float[3];

    [Header("Corazones")]
    [Tooltip("Sprite del corazón vacío")]
    [SerializeField]
    private Sprite attemptsEmptyHeartSprite = null;
    [Tooltip("GameObjects de los corazones")]
    [SerializeField]
    private GameObject[] attemptsHearts = new GameObject[3];

    [Header("EndGame")]
    [Tooltip("Imagen de Marshallow")]
    [SerializeField]
    private Image marshallowImage = null;
    [Tooltip("Sprite de Marshallow al ganar")]
    [SerializeField]
    private Sprite winMarshallowSprite = null;
    [Tooltip("Sprite de Marshallow al perder")]
    [SerializeField]
    private Sprite loseMarshallowSprite = null;
    [Tooltip("Texto del endgame")]
    [SerializeField]
    private TextMeshProUGUI endGameText = null;
    [Tooltip("Texto del tiempo")]
    [SerializeField]
    private TextMeshProUGUI timeText = null;
    [Tooltip("Contenedor para los VFX")]
    [SerializeField]
    private GameObject endGameVFX = null;

    [Header("Robos")]
    [Tooltip("Objeto padre de los robos")]
    [SerializeField]
    private GameObject robberiesParent = null;
    [Tooltip("Prefab de los robos")]
    [SerializeField]
    private GameObject robberyPrefab = null;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Awake, que se ejecuta cuando carga el script
    /// </summary>
    void Awake()
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
    /// Método UpdateRobberiesText, que actualiza el texto correspondiente a los robos restantes
    /// </summary>
    /// <param name="robberiesLeft">Contador de robos</param>
    /// <param name="maxRobberies">Máximo de robos</param>
    public void UpdateRobberiesText(int robberiesLeft, int maxRobberies)
    {
        robberiesText.text = LocalizationSystem.GetLocalizedValue("THIEF_ROBBERIES") +
            ": " + robberiesLeft + "/" + maxRobberies;
    }

    /// <summary>
    /// Método InitializeAttempts, que dibuja los intentos al empezar
    /// </summary>
    /// <param name="attempts"></param>
    public void InitializeAttempts(Difficulty difficulty)
    {
        // Desactivamos los corazones innecesarios
        for (int i = difficulty.catchAttempts; i < attemptsHearts.Length; i++)
        {
            attemptsHearts[i].SetActive(false);
        }

        // Reescalamos el background de los corazones
        //attemptsBackground.sizeDelta = new Vector2(backgroundWidths[difficulty.difficultyIndex], attemptsBackground.rect.height);
        attemptsBackground.localScale = new Vector3(backgroundWidths[difficulty.difficultyIndex], 1.0f, 1.0f);
    }

    /// <summary>
    /// Método UpdateAttempts, que actualiza los intentos restantes
    /// </summary>
    /// <param name="attempts">Intentos restantes</param>
    public void UpdateAttempts(int attempts)
    {
        // Cambiamos el sprite del último corazón
        attemptsHearts[attempts].GetComponent<Image>().sprite = attemptsEmptyHeartSprite;
    }

    /// <summary>
    /// Método UpdateEndGameScreen, que actualiza la UI de la pantalla final
    /// </summary>
    /// <param name="hasWin">Booleano que indica si ha ganado o perdido</param>
    /// <param name="totalTime">Tiempo total de la partida</param>
    public void UpdateEndGameScreen(bool hasWin, float totalTime)
    {
        // Si ha ganado
        if (hasWin)
        {
            // Texto de arriba
            endGameText.text = LocalizationSystem.GetLocalizedValue("ENDGAME_WIN");

            // Sprite Marshallow
            marshallowImage.sprite = winMarshallowSprite;

            // Efecto de victoria
            GameObject victoryVFX = Instantiate(GameManager.instance.victoryVFX, endGameVFX.transform);

            // Texto del tiempo
            int seconds = (int)(totalTime % 60);
            int minutes = (int)((totalTime / 60) % 60);
            string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);

            timeText.text = LocalizationSystem.GetLocalizedValue("TIME") + ": " + timerString;
        }
        // Si ha perdido
        else
        {
            // Texto de arriba
            endGameText.text = LocalizationSystem.GetLocalizedValue("ENDGAME_LOSE");

            // Sprite Marshallow
            marshallowImage.sprite = loseMarshallowSprite;

            // Efecto de derrota
            GameObject defeatVFX = Instantiate(GameManager.instance.defeatVFX, endGameVFX.transform);

            // Texto del tiempo
            timeText.gameObject.SetActive(false);
        }
    }

    public void ShowRobberyIcon(Vector3 location)
    {
        GameObject robberyGameObject = Instantiate(robberyPrefab, robberiesParent.transform);
        Robbery newRobbery = robberyGameObject.GetComponent<Robbery>();
        newRobbery.robberyPosition = location;
        newRobbery.robberyRectTransform.anchoredPosition = new Vector2(100000, 100000);
        GameManager.instance.robberies.Add(newRobbery);
    }

    public void HideRobberyIcon(Robbery robbery)
    {
        // Borramos el robo de la lista de robos
        GameManager.instance.robberies.Remove(robbery);

        // Eliminamos el GameObject del robo
        Destroy(robbery.gameObject);
    }
    #endregion
}