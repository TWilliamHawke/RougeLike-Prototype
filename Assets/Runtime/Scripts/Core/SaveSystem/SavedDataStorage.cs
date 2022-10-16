using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

namespace Core.SaveSystem
{
    public class SavedDataStorage : ISaveManager, ILoadManager, IPermanentDependency
    {
        Injector _selfInjector;

        public SavedDataStorage(Injector selfInjector)
        {
            _selfInjector = selfInjector;
        }

        Dictionary<string, string> _serializedData = new();
        List<IHaveSaveState> _saveDataOwners = new();

        string _separator = ";;";

        public void LoadGame(string path)
        {
            var sr = new StreamReader(path);
            var b64 = sr.ReadToEnd();
            var bytes = System.Convert.FromBase64String(b64);
            string str = Encoding.UTF8.GetString(bytes);
            var keyValueList = str.Split(_separator);
            _serializedData.Clear();

            for (int i = 0; i < keyValueList.Length - 1; i += 2)
            {
                var key = keyValueList[i];
                var value = keyValueList[i + 1];
                _serializedData[key] = value;
            }

            _selfInjector.AddDependency(this);
        }

        public void SaveGame(string path)
        {
            foreach (var saveDataOwner in _saveDataOwners)
            {
                saveDataOwner.Save(this);
            }

            var stringBuilder = new StringBuilder();

            foreach (var pair in _serializedData)
            {
                stringBuilder.Append(pair.Key);
                stringBuilder.Append(_separator);
                stringBuilder.Append(pair.Value);
                stringBuilder.Append(_separator);
            }

            var textbytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
            var b64 = System.Convert.ToBase64String(textbytes);
            using (var sw = new StreamWriter(path))
            {
                sw.WriteLine(b64);
            }
        }

        public void Register(IHaveSaveState saveDataOwner)
        {
            _saveDataOwners.Add(saveDataOwner);
            saveDataOwner.Load(this);
        }

        void ILoadManager.GetSaveState<T>(string key, ref T data)
        {
            if (_serializedData.TryGetValue(key, out var json))
            {
                var saveData = JsonUtility.FromJson<SavedData<T>>(json);
                if (saveData == null) return;
                data = saveData.data;
            }
        }

        void ISaveManager.AddSaveState<T>(string key, T stringifyData)
        {
            var saveData = new SavedData<T>();
            var jsonData = JsonUtility.ToJson(stringifyData);

            saveData.data = stringifyData;

            var json = JsonUtility.ToJson(saveData);
            _serializedData[key] = json;
        }

        public void ClearState()
        {
            _serializedData.Clear();
            _saveDataOwners.Clear();
        }

        [System.Serializable]
        public class SavedData<T>
        {
            public T data;
        }
    }

}