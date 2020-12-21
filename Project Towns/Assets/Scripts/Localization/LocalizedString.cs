/// <summary>
/// Estructura LocalizedString, para guardar la clave de la cadena localizada
/// </summary>
[System.Serializable]
public struct LocalizedString
{
    public string key;

    public LocalizedString(string key)
    {
        this.key = key;
    }

    public string value
    {
        get
        {
            return LocalizationSystem.GetLocalizedValue(key);
        }
    }

    public static implicit operator LocalizedString(string key)
    {
        return new LocalizedString(key);
    }
}
