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

    public static void Open(string searchquery)
    {
        var window = GetWindow<LocalisationWindow>();
        window._searchString = searchquery;
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
        if (GUILayout.Button("Refresh", GUILayout.MaxWidth(80)))
        {

        }
        EditorGUILayout.LabelField("Filter by: ", GUILayout.MaxWidth(50));
        _filterType = (FilterType)EditorGUILayout.EnumPopup(_filterType, GUILayout.MaxWidth(55));

        _searchString = EditorGUILayout.TextField(_searchString, GUILayout.MaxWidth(150));

        if (GUILayout.Button(new GUIContent("X", "Clear search"), GUILayout.MaxWidth(20)))
        {
            _searchString = "";
        }

        if (GUILayout.Button(new GUIContent("<", "Erase last character"), GUILayout.MaxWidth(20)))
        {
            _searchString = _searchString.Substring(0, _searchString.Length - 1);
        }
        if (GUILayout.Button(new GUIContent("Open", "Manual file edit"), GUILayout.MaxWidth(50)))
        {
            EditorUtility.OpenWithDefaultApp("Assets/Resources/localisation.tsv");
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

