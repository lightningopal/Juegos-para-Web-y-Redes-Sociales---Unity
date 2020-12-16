using UnityEngine;

//Class MenuManager, to control esc key navegation and start the MainTheme.
public class MenuManager : MonoBehaviour
{
    /*public GameObject mainMenu;
    public GameObject battleMenu;
    public GameObject rankings1;
    public GameObject rankings2;
    public GameObject rankings3;
    public GameObject difficultyMenu;
    public GameObject [] tutorial = new GameObject[9];
    public GameObject options;
    public GameObject controlsEdit;
    public GameObject imageOptsMenu;
    public GameObject soundOptsMenu;
    public GameObject creditsMenu;

    public GameObject background;
    private float mousePosX, mousePosY;

    //On start, plays the MainTheme.
    void Start()
    {
        foreach (Sound s in AudioManager.instance.music)
        {
            AudioManager.instance.ManageAudio(s.name, "music", "stop");
            AudioManager.instance.ManageAudio(s.name, "music", "unpause");
        }
        AudioManager.instance.ManageAudio("MainTheme", "music", "play");
    }

    //We check every frame.
    void Update()
    {
        //Move the background.
        mousePosX = Input.mousePosition.x;
        mousePosY = Input.mousePosition.y;
        background.GetComponent<RectTransform>().position = new Vector2((mousePosX / Screen.width) * 20 + (Screen.width / 2) - 10, (mousePosY / Screen.height) * 20 + (Screen.height / 2) - 10);

        //When press Escape key on Main Menu, game closes. Else, go back.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainMenu.activeSelf)
            {
                Application.Quit();
                AudioManager.instance.PlaySound("Button");
            }
            else if (battleMenu.activeSelf)
            {
                battleMenu.SetActive(false);
                mainMenu.SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (rankings1.activeSelf)
            {
                rankings1.SetActive(false);
                battleMenu.SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (rankings2.activeSelf)
            {
                rankings2.SetActive(false);
                rankings1.SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (rankings3.activeSelf)
            {
                rankings3.SetActive(false);
                rankings2.SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (difficultyMenu.activeSelf)
            {
                difficultyMenu.SetActive(false);
                battleMenu.SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (tutorial[0].activeSelf)
            {
                tutorial[0].SetActive(false);
                mainMenu.SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (tutorial[1].activeSelf)
            {
                tutorial[1].SetActive(false);
                tutorial[0].SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (tutorial[2].activeSelf)
            {
                tutorial[2].SetActive(false);
                tutorial[1].SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (tutorial[3].activeSelf)
            {
                tutorial[3].SetActive(false);
                tutorial[2].SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (tutorial[4].activeSelf)
            {
                tutorial[4].SetActive(false);
                tutorial[3].SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (tutorial[5].activeSelf)
            {
                tutorial[5].SetActive(false);
                tutorial[4].SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (tutorial[6].activeSelf)
            {
                tutorial[6].SetActive(false);
                tutorial[5].SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (tutorial[7].activeSelf)
            {
                tutorial[7].SetActive(false);
                tutorial[6].SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (options.activeSelf)
            {
                options.SetActive(false);
                mainMenu.SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (controlsEdit.activeSelf)
            {
                controlsEdit.SetActive(false);
                options.SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (imageOptsMenu.activeSelf)
            {
                imageOptsMenu.SetActive(false);
                options.SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (soundOptsMenu.activeSelf)
            {
                soundOptsMenu.SetActive(false);
                options.SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
            else if (creditsMenu.activeSelf)
            {
                creditsMenu.SetActive(false);
                mainMenu.SetActive(true);
                AudioManager.instance.PlaySound("Button");
            }
        }
    }*/
}
