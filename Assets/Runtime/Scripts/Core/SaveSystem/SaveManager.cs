using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core.SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] Injector _dataStorageInjector;

        SavedDataStorage _savedDataStorage;

        const string SAVE_FOLDER_NAME = "Test";

        void Awake()
        {
            _savedDataStorage = new SavedDataStorage(_dataStorageInjector);
            TryCreateSaveFolder();
            _savedDataStorage.LoadGame(GetFullPath("s1.sav"));
        }

        void OnDisable()
        {
            _savedDataStorage.SaveGame(GetFullPath("s1.sav"));
        }

        private void TryCreateSaveFolder()
        {
            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var myGamesFolder = Path.Combine(documentsFolder, "My Games");
            var savesFolder = Path.Combine(myGamesFolder, SAVE_FOLDER_NAME);

            if (!Directory.Exists(myGamesFolder))
            {
                Directory.CreateDirectory(myGamesFolder);
            }

            if (!Directory.Exists(savesFolder))
            {
                Directory.CreateDirectory(savesFolder);
            }
        }

        private string GetFullPath(string filename)
        {
            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(documentsFolder, "My Games", SAVE_FOLDER_NAME, filename);
        }

    }
}
