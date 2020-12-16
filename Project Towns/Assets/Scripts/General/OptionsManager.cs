using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Class OptionsManager, to control the options on each scene.
public class OptionsManager : MonoBehaviour
{
    /*public TextMeshProUGUI controlsText;
    public Slider musicSlider;
    public Slider fxSlider;
    public Slider brightnessSlider;

    public float musicVolume;
    public float fxVolume;

    public float brightnessLvl;
    public bool fullScreen;

    public Image brightnessImage;
    public Image buttonFullScreen;
    public Sprite buttonFSOn;
    public Sprite buttonFSOff;

    Resolution[] resolutions;
    public TMP_Dropdown resolutionsDropdown;
    int currentResolutionIndex;

    private GameObject currentKey;
    public TextMeshProUGUI arrowUp, arrowDown, arrowLeft, arrowRight, street, shop, troop;
    private Color32 normalColor = new Color32(255, 255, 255, 255);
    private Color32 selectedColor = new Color32(64, 64, 64, 255);
    private bool usedKey;

    public GameObject buttons;
    Scene scene;

    public static OptionsManager instance;

    //On Start, get the options from GlobalVars, search for resolutions and
    //set texts and values to the GlobalVars values.
    void Start()
    {
        instance = this;

        scene = SceneManager.GetActiveScene();

        musicVolume = GlobalVars.globalVars.musicVolume;
        fxVolume = GlobalVars.globalVars.fxVolume;
        brightnessLvl = GlobalVars.globalVars.brightnessLvl;
        fullScreen = GlobalVars.globalVars.fullScreen;

        char[] auxArrow = { 'A', 'r', 'r', 'o', 'w' };
        arrowUp.text = GlobalVars.globalVars.keyBinds["ArrowUp"].ToString();
        if (arrowUp.text.Contains("Arrow"))
        {
            arrowUp.text = GlobalVars.globalVars.keyBinds["ArrowUp"].ToString().TrimEnd(auxArrow) + "\nArrow";
        }

        arrowDown.text = GlobalVars.globalVars.keyBinds["ArrowDown"].ToString();
        if (arrowDown.text.Contains("Arrow"))
        {
            arrowDown.text = GlobalVars.globalVars.keyBinds["ArrowDown"].ToString().TrimEnd(auxArrow) + "\nArrow";
        }

        arrowLeft.text = GlobalVars.globalVars.keyBinds["ArrowLeft"].ToString();
        if (arrowLeft.text.Contains("Arrow"))
        {
            arrowLeft.text = GlobalVars.globalVars.keyBinds["ArrowLeft"].ToString().TrimEnd(auxArrow) + "\nArrow";
        }

        arrowRight.text = GlobalVars.globalVars.keyBinds["ArrowRight"].ToString();
        if (arrowRight.text.Contains("Arrow"))
        {
            arrowRight.text = GlobalVars.globalVars.keyBinds["ArrowRight"].ToString().TrimEnd(auxArrow) + "\nArrow";
        }

        street.text = GlobalVars.globalVars.keyBinds["StreetKeyC"].ToString();
        shop.text = GlobalVars.globalVars.keyBinds["ShopKeyC"].ToString();
        troop.text = GlobalVars.globalVars.keyBinds["TroopKeyC"].ToString();

        TextMeshProUGUI[] remapTexts = { arrowUp, arrowDown, arrowLeft, arrowRight, street, shop, troop };

        foreach (TextMeshProUGUI t in remapTexts)
        {
            if (t.text.Length > 1)
                t.fontSize = 132;
            else
                t.fontSize = 250;
        }

        currentResolutionIndex = -1;
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();

        List<string> resOptions = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string rOption = resolutions[i].width + " X " + resolutions[i].height;
            resOptions.Add(rOption);

            if (GlobalVars.globalVars.resolution.width == resolutions[i].width && GlobalVars.globalVars.resolution.height == resolutions[i].height)
            {
                currentResolutionIndex = i;
            }
            else if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height && currentResolutionIndex == -1)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropdown.AddOptions(resOptions);

        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

        musicSlider.value = musicVolume;
        fxSlider.value = fxVolume;
        brightnessSlider.value = brightnessLvl;
        Screen.fullScreen = fullScreen;

        if (fullScreen)
            buttonFullScreen.sprite = buttonFSOn;
        else
            buttonFullScreen.sprite = buttonFSOff;
    }

    //Set the music volume.
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        GlobalVars.globalVars.musicVolume = musicVolume;
        foreach (Sound s in AudioManager.instance.music)
        {
            s.source.volume = musicVolume;
        }
        writeOptions();
    }

    //Set the sound fx volume.
    public void SetFXVolume(float volume)
    {
        fxVolume = volume;
        GlobalVars.globalVars.fxVolume = fxVolume;
        foreach (Sound s in AudioManager.instance.sounds)
        {
            s.source.volume = fxVolume;
        }
        writeOptions();
    }

    //Set the brightness level.
    public void SetBrightness(float brightness)
    {
        brightnessLvl = brightness;
        GlobalVars.globalVars.brightnessLvl = brightnessLvl;

        if (brightness > 0.5)
            brightnessImage.color = new Vector4(1.0f, 1.0f, 1.0f, brightness - 0.5f);
        else
            brightnessImage.color = new Vector4(0.0f, 0.0f, 0.0f, 0.5f - brightness);

        writeOptions();
    }

    //Activate or deactivate the fullscreen.
    public void ToggleFullScreen()
    {
        GlobalVars.globalVars.fullScreen = !GlobalVars.globalVars.fullScreen;
        fullScreen = !fullScreen;
        Screen.fullScreen = fullScreen;
        if (fullScreen)
        {
            buttonFullScreen.sprite = buttonFSOn;
        }
        else
        {
            buttonFullScreen.sprite = buttonFSOff;
        }
        writeOptions();
    }

    //Set the resolution to selected.
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        GlobalVars.globalVars.resolution.width = resolution.width;
        GlobalVars.globalVars.resolution.height = resolution.height;
        currentResolutionIndex = resolutionIndex;

        writeOptions();
    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                foreach (KeyValuePair<string, KeyCode> entry in GlobalVars.globalVars.keyBinds)
                {
                    if (e.keyCode == entry.Value)
                    {
                        usedKey = true;
                    }
                }

                if (!usedKey)
                {
                    if (e.keyCode != KeyCode.Escape)
                    {
                        GlobalVars.globalVars.keyBinds[currentKey.name] = e.keyCode;

                        if (!scene.name.Equals("MainMenu"))
                        {
                            ButtonController[] allButtons = buttons.GetComponentsInChildren<ButtonController>();
                            foreach (ButtonController b in allButtons)
                            {
                                b.updateKeys();
                            }

                            NoteObject[] allNotes = BeatScroller.instance.GetComponentsInChildren<NoteObject>();
                            foreach (NoteObject n in allNotes)
                            {
                                n.updateKeys();
                            }
                        }

                        currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                        if (e.keyCode.ToString().Contains("Arrow"))
                        {
                            char[] auxArrow = { 'A', 'r', 'r', 'o', 'w' };
                            currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString().TrimEnd(auxArrow) + "\nArrow";
                        }

                        if (e.keyCode.ToString().Length > 1)
                            currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 132;
                        else
                            currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 250;
                        writeOptions();
                    }
                }
                currentKey.GetComponent<Image>().color = normalColor;
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normalColor;
        }

        currentKey = clicked;
        usedKey = false;
        currentKey.GetComponent<Image>().color = selectedColor;
    }

    //Write the actual options.
    public void writeOptions()
    {
        //Path the our text file.
        string path = "./Dance Kingdom_Data/Resources/options.txt";

        //Erase all content of the text file.
        System.IO.File.WriteAllText(path, string.Empty);

        //Writer to write on our text file.
        StreamWriter writer = new StreamWriter(path, true);

        //We write our options in the text file.
        writer.WriteLine("musicVolume:" + musicVolume);
        writer.WriteLine("fxVolume:" + fxVolume);
        writer.WriteLine("brightnessLvl:" + brightnessLvl);
        writer.WriteLine("fullScreen:" + fullScreen);
        writer.WriteLine("resolution:" + resolutions[currentResolutionIndex].width + ":" + resolutions[currentResolutionIndex].height);
        writer.WriteLine("GlobalVars.globalVars.keyBinds:street:" + GlobalVars.globalVars.keyBinds["StreetKeyC"] + ":shop:" + GlobalVars.globalVars.keyBinds["ShopKeyC"] + ":troop:" + GlobalVars.globalVars.keyBinds["TroopKeyC"] +
                ":aUp:" + GlobalVars.globalVars.keyBinds["ArrowUp"] + ":aDown:" + GlobalVars.globalVars.keyBinds["ArrowDown"] + ":aLeft:" + GlobalVars.globalVars.keyBinds["ArrowLeft"] + ":aRight:" + GlobalVars.globalVars.keyBinds["ArrowRight"]);

        //Close the writer.
        writer.Close();
    }*/
}
