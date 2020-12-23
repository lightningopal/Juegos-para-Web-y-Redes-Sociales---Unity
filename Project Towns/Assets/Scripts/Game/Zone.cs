using UnityEngine;

/// <summary>
/// Clase Zone, que guarda información sobre una zona
/// </summary>
[System.Serializable]
public class Zone
{
    #region Variables
    // Nombre de la zona
    public string zoneName;

    // GameObject que define la zona
    public GameObject zoneFloor;
    #endregion

    #region Constructores
    public Zone()
    {

    }

    public Zone(string zoneName_, GameObject zoneFloor_)
    {
        zoneName = zoneName_;
        zoneFloor = zoneFloor_;
    }
    #endregion
}