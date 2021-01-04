using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase InformationObject, que se utiliza para mostrar información de los objetos
/// </summary>
public class InformationObject : MonoBehaviour
{
    #region Variables
    [Header("Items")]
    [Tooltip("Primer item")]
    public Item item1;
    [Tooltip("SpriteRenderer del primer item")]
    public SpriteRenderer item1Sprite;
    [Tooltip("Segundo item")]
    public Item item2;
    [Tooltip("SpriteRenderer del segundo item")]
    public SpriteRenderer item2Sprite;
    [Tooltip("Booleano que indica si un objeto es dudoso")]
    public bool doubtfulInformation;
    [Tooltip("Posicion del item1 cuando está solo")]
    [SerializeField]
    private float item1PositionWhenOnly = 0;
    [Tooltip("Posicion del item1 cuando son 2")]
    [SerializeField]
    private float item1PositionWhenTwo = 30;

    [Header("Backgrounds")]
    [Tooltip("SpriteRenderer del background")]
    [SerializeField]
    private SpriteRenderer backgroundImage = null;
    [Tooltip("Background para un item")]
    [SerializeField]
    private Sprite[] backgroundsOneItem = new Sprite[2];
    [Tooltip("Background para dos items")]
    [SerializeField]
    private Sprite[] backgroundsTwoItems = new Sprite[2];

    //Camara principal
    private Camera mainCamera;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {
        this.transform.LookAt(mainCamera.transform);
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método Calculate Information, que muestra la información de los objetos
    /// </summary>
    public void CalculateInformation()
    {
        item1Sprite.sprite = item1.itemSprite;
        // Si son dos objetos
        if (item2 != null)
        {
            item2Sprite.sprite = item2.itemSprite;

            backgroundImage.sprite = (!doubtfulInformation) ? backgroundsTwoItems[0] : backgroundsTwoItems[1];
            item1Sprite.transform.localPosition = new Vector3(item1Sprite.transform.localPosition.x, item1PositionWhenTwo, 0);
            item2Sprite.gameObject.SetActive(true);
        }
        // Si es un objeto
        else
        {
            backgroundImage.sprite = (!doubtfulInformation) ? backgroundsOneItem[0] : backgroundsOneItem[1];
            item1Sprite.transform.localPosition = new Vector3(item1Sprite.transform.localPosition.x, item1PositionWhenOnly, 0);
            item2Sprite.gameObject.SetActive(false);
        }
    }
    #endregion
}