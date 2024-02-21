using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Localisation;
using System;

public class LocalisationWindow : EditorWindow
{
    GameLanguages _gameLanguage = GameLanguages.english;
    TSVEditor _CSVReader;
    Vector2 scrollPos;

    [MenuItem("Window/Localisation Window")]
    public static void Open()
    {
        var window = GetWindow<LocalisationWindow>();
    }

    void OnGUI()
    {
        DrawLanguageSelector();

        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, true);

        foreach (var line in TSVEditor.data)
        {
            DrawLine(line);
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

    }

    private void DrawLine(string[] elements)
    {
        EditorGUILayout.BeginHorizontal();
        string key = elements[0];
        if (GUILayout.Button("Edit", GUILayout.MaxWidth(50)))
        {
            EditLocalStringWindow.Open(key, _gameLanguage, EditLocalStringWindow.WindowMode.edit);
        }

        EditorGUILayout.LabelField(key);
        EditorGUILayout.LabelField(elements[(int)_gameLanguage]);
        EditorGUILayout.EndHorizontal();
    }

    private void DrawLanguageSelector()
    {
        var oldLang = _gameLanguage;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical(GUI.skin.label, GUILayout.Height(15), GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        _gameLanguage = (GameLanguages)EditorGUILayout.EnumPopup(_gameLanguage);


        EditorGUILayout.EndVertical();
        if (GUILayout.Button("Refresh", GUILayout.MaxWidth(150)))
        {

        }
        EditorGUILayout.EndHorizontal();

        if (oldLang == _gameLanguage) return;
    }

}

