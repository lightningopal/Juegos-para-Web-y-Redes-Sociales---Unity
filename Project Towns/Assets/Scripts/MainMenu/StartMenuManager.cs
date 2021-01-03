using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    #region Variables
    public LocalizedText text;
    public Image image;
    public Sprite[] images = new Sprite[2];
    #endregion

    #region MétodosUnity
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isMobilePlatform)
        {
            image.sprite = images[1];
            text.ChangeText("START_MOBILE");
        }
        else
        {
            image.sprite = images[0];
            text.ChangeText("START_DESKTOP");
        }
    }
    #endregion
}
