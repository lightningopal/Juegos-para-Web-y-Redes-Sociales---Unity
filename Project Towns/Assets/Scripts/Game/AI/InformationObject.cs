using UnityEngine;

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
    [Tooltip("Booleano que indica si son dos items")]
    public bool areTwoItems = false;
    [Tooltip("Posicion del item1 cuando está solo")]
    [SerializeField]
    private float item1PositionWhenOnly = 0;
    [Tooltip("Posicion del item1 cuando son 2")]
    [SerializeField]
    private float item1PositionWhenTwo = 1;

    [Header("Backgrounds")]
    [Tooltip("Background for only one item")]
    [SerializeField]
    private Sprite backgroundOneItem = null;
    [Tooltip("Background for two items")]
    [SerializeField]
    private Sprite backgroundTwoItems = null;

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
    /// Método ShowInformation, que muestra la información de los objetos
    /// </summary>
    public void ShowInformation()
    {
        if (areTwoItems)
        {
            this.GetComponent<SpriteRenderer>().sprite = backgroundTwoItems;
            item1Sprite.transform.position = new Vector3(
                item1Sprite.transform.position.x, item1PositionWhenTwo, item1Sprite.transform.position.z);
            item2Sprite.gameObject.SetActive(true);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = backgroundOneItem;
            item1Sprite.transform.position = new Vector3(
                item1Sprite.transform.position.x, item1PositionWhenOnly, item1Sprite.transform.position.z);
            item2Sprite.gameObject.SetActive(false);
        }
    }
    #endregion
}