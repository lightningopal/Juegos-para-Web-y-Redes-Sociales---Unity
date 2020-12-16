using UnityEngine;

/// <summary>
/// Clase GlobalVars, para almacenar información entre escenas
/// </summary>
public class GlobalVars : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    [HideInInspector]
    public static GlobalVars globalVars;

    public float musicVolume;
    public float fxVolume;
    public int languageIndex;

    //public float brightnessLvl;
    //public bool fullScreen;
    //public Resolution resolution;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Awake, que se llama al cargar el script
    /// </summary>
    void Awake()
    {
        if (globalVars == null)
        {
            globalVars = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        ReadOptions();
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método ReadOptions, que lee las opciones guardadas del jugador
    /// </summary>
    private void ReadOptions()
    {

    }
    #endregion
}
