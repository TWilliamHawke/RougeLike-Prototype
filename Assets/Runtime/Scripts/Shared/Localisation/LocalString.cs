using UnityEngine;

[System.Serializable]
public struct LocalString
{
    [SerializeField] string _locKey;

    public LocalString(string localKey)
    {
        _locKey = localKey;
    }

    public string GetLocalText(params TextReplacer[] replacers)
    {
        return LocalDictionary.GetLocalisedString(_locKey, replacers);
    }

    public static implicit operator string(LocalString localString)
    {
        return localString.GetLocalText();
    }

    public static implicit operator LocalString(string localString)
    {
        return new LocalString(localString);
    }
}