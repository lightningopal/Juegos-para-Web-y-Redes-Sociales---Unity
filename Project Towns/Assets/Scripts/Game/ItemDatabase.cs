using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase ItemDatabase, que guarda los distintos Items del juego
/// </summary>
public class ItemDatabase : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    public static ItemDatabase instance;

    [Tooltip("Colores de los personajes")]
    public MaterialItem[] characterColors = new MaterialItem[5];

    [Tooltip("Objetos de la cabeza")]
    public List<Item> headItems = new List<Item>();
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

}