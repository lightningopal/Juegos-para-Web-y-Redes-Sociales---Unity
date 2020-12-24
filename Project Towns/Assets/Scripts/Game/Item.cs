using UnityEngine;

/// <summary>
/// Clase Item, que contiene la información de un item
/// </summary>
[System.Serializable]
public class Item
{
    [Tooltip("GameObject del item")]
    public GameObject itemGameObject;

    [Tooltip("Sprite del item")]
    public Sprite itemSprite;
}

/// <summary>
/// Clase MaterialItem, que hereda de Item y contiene la información de un material
/// </summary>
[System.Serializable]
public class MaterialItem : Item
{
    [Tooltip("Material del item")]
    public Material itemMaterial;
}
