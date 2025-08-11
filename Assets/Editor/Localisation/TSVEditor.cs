using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Localisation
{
    public class TSVEditor : TSVReader
    {
        public static List<string[]> data { get; private set; }
        public static Dictionary<string, string[]> _dataByKey = new();
        public static readonly int normalSize = Enum.GetNames(typeof(GameLanguages)).Length + 1;
        const string targetPath = "Assets/Resources/localisation.tsv";

        static TSVEditor()
        {
            CreateData();
        }

        public static string[] SplitLine(string line)
        {
            string[] parts = line.Split(_cellSeparator);
            int oldSize = parts.Length;

            if (oldSize < normalSize)
            {
                Array.Resize(ref parts, normalSize);
            }

            Array.Fill(parts, parts[0], oldSize, normalSize - oldSize);

            return parts;
        }

        public static bool TrySaveValues(string[] values)
        {
            if (values.Length < 2) return false;
            string key = values[0];
            if (string.IsNullOrEmpty(key)) return false;
            if (string.IsNullOrEmpty(values[1])) return false;

            for (int i = 2; i < values.Length; i++)
            {
                if (!string.IsNullOrEmpty(values[i])) continue;
                values[i] = values[1];
            }

            if (KeyIsExist(key))
            {
                ChangeLine(key, values);
            }
            else
            {
                AppendLine(values);
            }

            UnityEditor.AssetDatabase.Refresh();
            return true;
        }

        private static void AppendLine(string[] line)
        {
            string fullLine = string.Join("\t", line);
            File.AppendAllText(targetPath, string.Concat('\n', fullLine));
            data.Add(line);
            _dataByKey[line[0]] = line;
        }

        private static void ChangeLine(string key, string[] line)
        {
            for(int i = 0; i < data.Count; i++)
            {
                if (data[i][0] != key) continue;
                data[i] = line;
                break;
            }

            _dataByKey[key] = line;
            File.WriteAllLines(targetPath, data.Select(values => string.Join("\t", values)));
        }

        private static void CreateData()
        {
            data = GetLines()
                .Select(line => SplitLine(line))
                .ToList();
            data.ForEach(el => _dataByKey[el[0]] = el);
        }

        public static string[] GetValuesByKey(string key)
        {
            if (!_dataByKey.TryGetValue(key, out var values))
            {
                values = SplitLine(key);
            }

            return values;
        }

        public static bool KeyIsExist(string key)
        {
            return _dataByKey.ContainsKey(key);
        }

        public static string GetEnglishText(string key)
        {
            bool found = _dataByKey.TryGetValue(key, out var values);

            return found ? values[1] : key;
        }


    }

}