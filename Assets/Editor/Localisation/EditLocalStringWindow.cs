using System;
using System.Linq;
using Localisation;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EditLocalStringWindow : EditorWindow
{
    GameLanguages _gameLanguage = GameLanguages.english;
    string _errorMessage = "";
    WindowMode _mode;

    string _value
    {
        get => _parts[(int)_gameLanguage];
        set => _parts[(int)_gameLanguage] = value;
    }
    string _key
    {
        get => _parts[0];
        set => _parts[0] = value;
    }
    string[] _parts;

    public static void Open(string key, GameLanguages gameLanguage, WindowMode mode)
    {
        var window = GetWindow<EditLocalStringWindow>();
        window._gameLanguage = gameLanguage;
        window.SetValue(key);
        window._mode = mode;
    }

    void OnGUI()
    {
        DrawLanguageSelector();

        if (_mode == WindowMode.edit)
        {
            EditorGUILayout.LabelField($"Key:        {_key}");
        }
        else
        {
            _key = EditorGUILayout.TextField("Key: ", _key);
        }
        EditorGUILayout.LabelField("Value: ");
        EditorStyles.textArea.wordWrap = true;
        _value = EditorGUILayout.TextArea(_value, GUILayout.Width(400), GUILayout.Height(150));

        DrawEnglishText();
        if (_mode == WindowMode.add && TSVEditor.KeyIsExist(_key))
        {
            _errorMessage = "You cannot add this key already exists";
        }


        if (_errorMessage != "")
        {
            EditorGUILayout.HelpBox(_errorMessage, MessageType.Error);
        }

        DrawSaveButtons();

    }

    private void DrawSaveButtons()
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Save"))
        {
            ValidateAndSave();
        }
        if (GUILayout.Button("Save and close"))
        {
            ValidateAndSave();
            Close();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void ValidateAndSave()
    {
        if (String.IsNullOrEmpty(_parts[1]))
        {
            _errorMessage = "English text is empty";
            return;
        }

        if (String.IsNullOrEmpty(_key))
        {
            _errorMessage = "Key is empty";
            return;
        }

        for (int i = 2; i < _parts.Length; i++)
        {
            if (String.IsNullOrEmpty(_parts[i]) || _parts[i] == _key)
            {
                _parts[i] = _parts[1];
            }
        }

        if (TSVEditor.TrySaveValues(_parts))
        {
            _mode = WindowMode.edit;
        }
        else
        {
            _errorMessage = "Cannot to save value";
        }
    }

    private void DrawEnglishText()
    {
        if (_gameLanguage == GameLanguages.english) return;
        if (String.IsNullOrEmpty(_parts[1]))
        {
            _errorMessage = "English text is empty";
            return;
        }

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("English Text:");
        if (GUILayout.Button("Copy to Value"))
        {
            _parts[(int)_gameLanguage] = _parts[1];
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField(_parts[1]);
    }

    private void SetValue(string key)
    {
        _parts = TSVEditor.GetValuesByKey(key);
    }

    private void DrawLanguageSelector()
    {
        var oldLang = _gameLanguage;
        _gameLanguage = (GameLanguages)EditorGUILayout.EnumPopup(_gameLanguage, GUILayout.MaxWidth(250));

        if (oldLang == _gameLanguage) return;
    }

    public enum WindowMode
    {
        edit = 1,
        add = 2
    }

}

