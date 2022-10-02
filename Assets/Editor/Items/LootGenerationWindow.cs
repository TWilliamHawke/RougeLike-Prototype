using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Items;

public class LootGenerationWindow : EditorWindow
{

    List<LootTable> _lootTables;
    LootTable _selectedLootTable;
    ItemSection<Item> _loot = new ItemSection<Item>(-1);
    LootTable _buggedLootTable;

    [MenuItem("Window/Loot Generation Window")]
    public static void Open()
    {
        var window = GetWindow<LootGenerationWindow>();
        window.FindTables();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        DrawSidebar();

        EditorGUILayout.EndVertical();

        if (_selectedLootTable == null) return;

        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField($"Selected loot table:");
        EditorGUILayout.LabelField($"{_selectedLootTable.name}", EditorHelpers.HeaderStyle());


        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(300));
        if (GUILayout.Button("Generate Loot"))
        {
            _selectedLootTable.CheckErrors();
            _loot.Clear();
            _selectedLootTable.FillDataList(ref _loot);
        }
        if (GUILayout.Button("Check Errors"))
        {
            _selectedLootTable.CheckErrors();
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

    private void RenderLoot()
    {
        if(_buggedLootTable != null) return;

        foreach (var slotData in _loot)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(new GUIContent(slotData.item.icon.texture), GUILayout.Width(32), GUILayout.Height(32));
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField($"Item: {slotData.item.displayName}");
            EditorGUILayout.LabelField($"Count: {slotData.count.ToString()}");
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

        }
    }

    void DrawSidebar()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);

        foreach (var lootTable in _lootTables)
        {
            if (GUILayout.Button(lootTable.name, style))
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

    }

}
