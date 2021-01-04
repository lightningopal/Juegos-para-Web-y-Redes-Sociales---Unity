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

    [Header("Items")]
    [Tooltip("Colores de los personajes")]
    public MaterialItem[] characterColors = new MaterialItem[5];
    [Tooltip("Ojos de los personajes")]
    public List<EyesItem> eyes = new List<EyesItem>();
    [Tooltip("Sombreros")]
    public List<Item> hatItems = new List<Item>();
    [Tooltip("Cuernos")]
    public List<Item> hornItems = new List<Item>();
    [Tooltip("Objetos del cuello")]
    public List<Item> neckItems = new List<Item>();

    [Header("Otros")]
    [Tooltip("Sprites de no tener objeto")]
    public Sprite[] noItemSprites = new Sprite[3];
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