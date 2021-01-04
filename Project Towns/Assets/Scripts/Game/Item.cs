using UnityEngine;

/// <summary>
/// Clase Item, que contiene la información de un item
/// </summary>
[System.Serializable]
public class Item
{
    [Tooltip("Nombre del item")]
    public string itemName;

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

/// <summary>
/// Clase EyesItem, que hereda de Item y contiene la información del número de ojos
/// </summary>
[System.Serializable]
public class EyesItem : Item
{
    [Tooltip("Número de ojos")]
    public int eyesNumber;
}

/// <summary>
/// Estructura VillagerItems, para almacenar los items de los aldeanos
/// </summary>
[System.Serializable]
public struct NPCItems
{
    public MaterialItem villagerColor;
    public EyesItem eyes;
    public Item hatItem;
    public Item hornItem;
    public Item neckItem;

    public override string ToString()
    {
        string hatItemString = "NO", hornItemString = "NO", neckItemString = "NO";

        if (hatItem != null)
            hatItemString = hatItem.itemName;

        if (hornItem != null)
            hornItemString = hornItem.itemName;

        if (neckItem != null)
            neckItemString = neckItem.itemName;

        return villagerColor.itemName + " - " + eyes.eyesNumber + " - " + hatItemString + " - " +
            hornItemString + " - " + neckItemString;
    }
}
