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
    string _searchString = "";
    FilterType _filterType = FilterType.key;

    [MenuItem("Window/Localisation Window")]
    public static void Open()
    {
        var window = GetWindow<LocalisationWindow>();
    }

    void OnGUI()
    {
        DrawLanguageSelector();
        int filterIdx = -1;
        if (_searchString != "")
        {
            filterIdx = _filterType == FilterType.key ? 0 : (int)_gameLanguage;
        }

        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, true);

        for (int i = 0; i < TSVEditor.data.Count; i++)
        {
            string[] line = TSVEditor.data[i];
            if (filterIdx >= 0 && filterIdx < line.Length && !line[filterIdx].Contains(_searchString)) continue;
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
        EditorGUILayout.LabelField("Filter by: ", GUILayout.MaxWidth(50));
        _filterType = (FilterType)EditorGUILayout.EnumPopup(_filterType, GUILayout.MaxWidth(55));

        _searchString = EditorGUILayout.TextField(_searchString, GUILayout.MaxWidth(150));

        if (GUILayout.Button("X", GUILayout.MaxWidth(20)))
        {
            _searchString = "";
        }


        EditorGUILayout.EndHorizontal();

        if (oldLang == _gameLanguage) return;
    }


    enum FilterType
    {
        key = 0,
        value = 1,
    }
}

