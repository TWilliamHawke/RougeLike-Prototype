using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Localisation;

public static class LocalDictionary
{
    static Dictionary<string, string> _dictionary;
    static string _aRegexp = "%a";
    static string _bRegexp = "%b";

    static LocalDictionary()
    {
        var gameLang = PlayerPrefs.GetString("game_lang", "en");
        var localisationLoader = new CSVReader(gameLang);
        _dictionary = localisationLoader.CreateDictionary();
    }

    public static string GetLocalisedString(string key, string aReplacer = "", string bReplacer = "")
    {
        if (_dictionary.TryGetValue(key, out var value))
        {
            value = ReplaceText(value, _aRegexp, aReplacer);
            value = ReplaceText(value, _bRegexp, bReplacer);
            return value;
        }
        else
        {
            Debug.Log($"{key} not found in localisation file");
            return key;
        }
    }

    static string ReplaceText(string original, string regexp, string replacer)
    {
        if (replacer is null || replacer == "") return original;
        return original.Replace(regexp, replacer);
    }



}


