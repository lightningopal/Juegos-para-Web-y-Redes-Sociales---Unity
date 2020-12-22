using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    public static UIManager instance;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Awake, that executes on script load
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

    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Método Update, que se llama cada frame
    /// </summary>
    void Update()
    {

    }
    #endregion

    #region MétodosClase
    #endregion
}
