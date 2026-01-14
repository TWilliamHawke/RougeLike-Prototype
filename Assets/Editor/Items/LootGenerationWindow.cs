using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Items;

public class LootGenerationWindow : EditorWindow
{

    List<LootTable> _lootTables;
    LootTable _selectedLootTable;
    ItemSection _loot = new ItemSection();
    LootTable _buggedLootTable;
    string _searchString = "";
    Vector2 _scrollPosition;

    [MenuItem("Window/Loot Generation Window")]
    public static void Open()
    {
        var window = GetWindow<LootGenerationWindow>();
        window.FindTables();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        DrawSidebar();

        if (_selectedLootTable == null)
        {
            EditorGUILayout.EndHorizontal();
            return;
        }

        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField($"Selected loot table:");
        EditorGUILayout.LabelField($"{_selectedLootTable.name}", EditorHelpers.HeaderStyle());


        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(300));
        if (GUILayout.Button("Generate Loot"))
        {
            TryCheckErors();
            _loot.Clear();
            _selectedLootTable.FillItemSection(_loot);
        }
        if (GUILayout.Button("Check Errors"))
        {
            TryCheckErors();
        }
        if (GUILayout.Button("Open in Inpector"))
        {
            Selection.activeObject = _selectedLootTable;
        }
        EditorGUILayout.EndHorizontal();

        RenderLoot();

        if (_buggedLootTable != null)
        {
            var message = $"LootTable {_buggedLootTable.name} has cycling reference";
            EditorGUILayout.HelpBox(message, MessageType.Error);
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(200));

            if (GUILayout.Button("Open in Inpector"))
            {
                Selection.activeObject = _buggedLootTable;
            }
            if (GUILayout.Button("Hide Message"))
            {
                _buggedLootTable = null;
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

    }

    public void FindTables()
    {
        _lootTables = EditorHelpers.GetAllInstances<LootTable>();
    }

    private void TryCheckErors()
    {
        try
        {
            _selectedLootTable.CheckErrors();
        }
        catch (System.Exception)
        {
            _buggedLootTable = _selectedLootTable;
        }
    }

    private void RenderLoot()
    {
        if (_buggedLootTable != null) return;

        foreach (var slotData in _loot.GetItems())
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(new GUIContent(slotData.item.icon.texture), GUILayout.Width(32), GUILayout.Height(32));
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField($"Item: {slotData.item.displayName}");
            EditorGUILayout.LabelField($"Count: {slotData.count}");
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

        }
    }

    void DrawSidebar()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(220), GUILayout.ExpandHeight(true));

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        GUIStyle style = new GUIStyle(GUI.skin.button);

        _searchString = EditorGUILayout.TextField(_searchString, GUILayout.MaxWidth(200));

        foreach (var lootTable in _lootTables)
        {
            if (lootTable.name.ToLower().Contains(_searchString.ToLower()) && GUILayout.Button(lootTable.name, style))
            {
                _selectedLootTable = lootTable;
            }
        }

        EditorGUILayout.LabelField("", GUI.skin.label);
        if (GUILayout.Button("Check errors for all"))
        {
            try
            {
                foreach (var lootTable in _lootTables)
                {
                    lootTable.CheckErrors();
                }

            }
            catch (LootTableException error)
            {
                _buggedLootTable = error.lootTable;
                _loot.Clear();
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}
