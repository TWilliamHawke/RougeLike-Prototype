using System.Collections.Generic;
using System;
using UnityEngine;
using System.Text;
using System.IO;
using System.Linq;

namespace Localisation
{
    public class TSVReader : ILocalisationLoader
    {
        static Dictionary<string, string> _dictionary;
        static protected char _lineSeparator = '\n';
        static protected string _cellSeparator = "\t";

        int _gameLanguageIdx = 1;

        public Dictionary<string, string> CreateDictionary()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> CreateDictionary(string gameLanguage)
        {
            Enum.TryParse<GameLanguages>(gameLanguage, out var _gameLanguage);
            return CreateDictionary(_gameLanguage);
        }


        public Dictionary<string, string> CreateDictionary(GameLanguages gameLanguage)
        {
            _dictionary = new();
            _gameLanguageIdx = (int)gameLanguage;

            var lines = GetLines();

            foreach (var line in lines)
            {
                try
                {
                    AddLineToDictionary(line);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }

            return _dictionary;
        }

        public static IEnumerable<string> GetLines()
        {
            TextAsset csv = Resources.Load<TextAsset>("localisation");

            var lines = csv.text
                .Split(_lineSeparator)
                .Select(line => line.Trim());

            return lines;
        }

        private void AddLineToDictionary(string line)
        {
            var keyValues = line.Split(_cellSeparator);
            string key = keyValues[0];
            string value = keyValues[_gameLanguageIdx];

            if (key == "") throw new System.Exception($"Found empty Key in line: {line}");
            if (value == "") throw new System.Exception($"Value for {key} is empty");
            if (_dictionary.ContainsKey(key)) throw new System.Exception($"Key {key} is already exist");

            _dictionary[key] = value;
        }

    }

}