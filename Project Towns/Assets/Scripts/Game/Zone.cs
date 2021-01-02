using UnityEngine;

/// <summary>
/// Clase Zone, que guarda información sobre una zona
/// </summary>
[System.Serializable]
public class Zone
{
    #region Variables
    [Tooltip("Nombre de la zona")]
    public string zoneName;

    [Tooltip("GameObject que define la zona")]
    public GameObject zoneFloor;

    [Tooltip("Punto de entrada a la zona")]
    public Transform enterPoint;

    [Tooltip("Número máximo de aldeanos en la zona")]
    public int maxVillagers;

    [Tooltip("Número de aldeanos en la zona")]
    public int villagerCount;
    #endregion

    #region Constructores
    public Zone()
    {

    }

    public Zone(string zoneName_, GameObject zoneFloor_, Transform enterPoint_, int maxVillagers_)
    {
        zoneName = zoneName_;
        zoneFloor = zoneFloor_;
        enterPoint = enterPoint_;
        maxVillagers = maxVillagers_;
    }
    #endregion
}