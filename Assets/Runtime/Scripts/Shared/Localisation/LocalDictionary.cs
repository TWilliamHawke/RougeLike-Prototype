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
        if (_dictionary.TryGetValue(key, out var value))
        {
            for(int i = 0; i < replacers.Length; i++)
            {
                var data = replacers[i];
                value = ReplaceText(value, data.pattern, data.replacer);
            }
            return value;
        }
        else
        {
            Debug.LogWarning($"{key} not found in localisation file");
            return key;
        }
    }

    static string ReplaceText(string original, string regexp, string replacer)
    {
        if (replacer is null || replacer == "") return original;
        return original.Replace(regexp, replacer);
    }

}


