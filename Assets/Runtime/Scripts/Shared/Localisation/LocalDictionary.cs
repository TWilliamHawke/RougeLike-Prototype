using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Localisation;

public static class LocalDictionary
{
    static Dictionary<string, string> _dictionary;

    static LocalDictionary()
    {
        var gameLang = PlayerPrefs.GetString("game_lang", "english");
        var localisationLoader = new TSVReader();
        _dictionary = localisationLoader.CreateDictionary(gameLang);
    }

    public static string GetLocalisedString(string key, params TextReplacer[] replacers)
    {
        //trygetvalue causes error if key not found
        if (!_dictionary.ContainsKey(key))
        {
            Debug.LogWarning($"{key} not found in localisation file");
            return key;
        }

        string localString = _dictionary[key];
        for (int i = 0; i < replacers.Length; i++)
        {
            var data = replacers[i];
            localString = ReplaceText(localString, data.pattern, data.replacer);
        }
        return localString;
    }

    static string ReplaceText(string original, string regexp, string replacer)
    {
        if (replacer is null || replacer == "") return original;
        return original.Replace(regexp, replacer);
    }

}


