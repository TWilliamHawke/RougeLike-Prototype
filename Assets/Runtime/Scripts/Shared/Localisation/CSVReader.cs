using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Localisation
{

    public class CSVReader : ILocalisationLoader
    {
        static Dictionary<string, string> _dictionary = new();
        static char _lineSeparator = '\n';
        static string _separatorRegexp = "\",\"";

        string _gameLanguage;

        public CSVReader(string gameLanguage)
        {
            _gameLanguage = gameLanguage;
        }

        public Dictionary<string, string> CreateDictionary()
        {

            TextAsset csv = Resources.Load<TextAsset>("localisation");

            var lines = csv.text.Split(_lineSeparator).Select(line => line.Trim().Trim('"'));
            var gameLangIndex = IndexOfGameLanguage(lines.FirstOrDefault());

            foreach (var line in lines)
            {
                try
                {
                    AddLineToDictionary(gameLangIndex, line);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }

            return _dictionary;
        }

        private void AddLineToDictionary(int gameLangIndex, string line)
        {
            var keyValues = line.Split(_separatorRegexp);
            var key = keyValues[0];
            var value = keyValues[gameLangIndex];

            if (key == "") throw new System.Exception($"Found empty Key in line: {line}");
            if (value == "") throw new System.Exception($"Value for {key} is empty");
            if (_dictionary.ContainsKey(key)) throw new System.Exception($"Key {key} is already exist");

            _dictionary[key] = value;
        }

        private int IndexOfGameLanguage(string languages_line)
        {
            var all_languages = languages_line.Split(_separatorRegexp);
            var gameLang = PlayerPrefs.GetString("game_lang", "en");
            var gameLangIndex = 1;

            for (int i = 0; i < all_languages.Length; i++)
            {
                if (all_languages[i] == _gameLanguage)
                {
                    gameLangIndex = i;
                }
            }

            return gameLangIndex;
        }
    }

}