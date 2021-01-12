using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyMenu : MonoBehaviour
{
    #region Variables
    [Tooltip("Options Manager")]
    [SerializeField]
    private OptionsManager optionsManager = null;

    [Tooltip("Botones")]
    [SerializeField]
    private Button[] difficultyButtons = new Button[3];
    [Tooltip("Botones de dificultad")]
    [SerializeField]
    private CustomDifficultyButton[] difficultyCustomButtons = new CustomDifficultyButton[3];
    [Tooltip("Textos de tap again")]
    [SerializeField]
    private TextMeshProUGUI[] difficultyButtonsTapText = new TextMeshProUGUI[3];

    private int mobileTapped = -1;
    #endregion

    #region MétodosClase
    public void StartGameOrTap(int difficulty)
    {
        if (!Application.isMobilePlatform)
        {
            optionsManager.StartGameWithDifficulty(difficulty);
        }
        else
        {
            // Si ya ha hecho tap
            if (mobileTapped == difficulty)
                optionsManager.StartGameWithDifficulty(difficulty);
            // Si no
            else
            {
                mobileTapped = difficulty;

                for (int i = 0; i < difficultyButtonsTapText.Length; i++)
                {
                    if (i == difficulty)
                    {
                        difficultyButtonsTapText[i].gameObject.SetActive(true);
                        difficultyButtons[i].Select();
                        difficultyCustomButtons[i].isSelected = true;
                    }
                }
            }
        }
    }

    public void ResetMobileTap()
    {
        mobileTapped = -1;
    }
    #endregion
}