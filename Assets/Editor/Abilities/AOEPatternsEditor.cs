using System.Collections.Generic;
using Abilities;
using CustomEditors;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(AOEPatterns))]
public class AOEPatternsEditor : SimpleEditor
{
    private const string HIT_ICON_STRING = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAABoSURBVDhPnY3BDcAgDAOZhS14dP1O0x2C/LBEgiNSHvfwyZabmV0jZRUpq2zi6f0DJwdcQOEdwwDLypF0zHLMa9+NQRxkQ+ACOT2STVw/q8eY1346ZlE54sYAhVhSDrjwFymrSFnD2gTZpls2OvFUHAAAAABJRU5ErkJggg==";

    private const string MIDDLE_ICON_STRING = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAZdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuMjHxIGmVAAAA50lEQVQ4T51Ruw6CQBCkwBYKWkIgQAs9gfgCvgb4BML/qWBM9Bdo9QPIuVOQ3JIzosVkc7Mzty9NCPE3lORaKMm1YA/LsnTXdbdhGJ6iKHoVRTEi+r4/OI6zN01Tl/XM7HneLsuyW13XU9u2ous6gYh3kiR327YPsp6ZgyDom6aZYFqiqqqJ8mdZz8xoca64BHjkZT0zY0aVcQbysp6Z4zj+Vvkp65mZttxjOSozdkEzD7KemekcxzRNHxDOHSDiQ/DIy3pmpjtuSJBThStGKMtyRKSOLnSm3DCMz3f+FUpyLZTkOgjtDSWORSDbpbmNAAAAAElFTkSuQmCC";

    const float ELEMENT_HEIGHT = 100f;
    const float SINGLE_LINE_HEIGHT = 18f;

    ReorderableList _reorderableList;
    protected static Texture2D _hitIcon;
    protected static Texture2D _targetIcon;
    protected static Texture2D _centerIcon;

    AOEPatterns tile => target as AOEPatterns;

    protected override void DrawProperties()
    {
        CreateTextures();
        _reorderableList = new ReorderableList(tile != null ? tile.rawPatterns : null, typeof(AttackPatternData), true, true, true, true);
        // m_ReorderableList.drawHeaderCallback = OnDrawHeader;
        _reorderableList.drawElementCallback = OnDrawElement;
        _reorderableList.elementHeightCallback = GetElementHeight;
        // m_ReorderableList.onChangedCallback = ListUpdated;
        // m_ReorderableList.onAddDropdownCallback = OnAddDropdownElement;
        _reorderableList.DoLayoutList();

        if (EditorGUI.EndChangeCheck())
        {
            SaveTile();
        }
    }

    private static void CreateTextures()
    {
        if (_hitIcon != null) return;
        _hitIcon = Base64ToTexture(HIT_ICON_STRING);
        _centerIcon = Base64ToTexture(MIDDLE_ICON_STRING);
        _targetIcon = SwitchRedAndGreen(_hitIcon);
    }

    private static Texture2D Base64ToTexture(string base64)
    {
        Texture2D t = new Texture2D(1, 1);
        t.hideFlags = HideFlags.HideAndDontSave;
        t.LoadImage(System.Convert.FromBase64String(base64));
        return t;
    }

    private static Texture2D SwitchRedAndGreen(Texture2D texture)
    {
        var newtexture = new Texture2D(15, 15, texture.format, false);
        Color32[] pixels = texture.GetPixels32();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i].g = pixels[i].r;
            pixels[i].r = pixels[i].b;
        }
        newtexture.SetPixels32(pixels);
        newtexture.Apply();
        return newtexture;
    }

    protected virtual void SaveTile()
    {
        EditorUtility.SetDirty(target);
        SceneView.RepaintAll();
    }

    private BoundsInt GetRuleGUIBounds()
    {
        BoundsInt bounds = new BoundsInt(Vector3Int.zero, Vector3Int.one);
        bounds.xMin = -2;
        bounds.yMin = -2;
        bounds.xMax = 3;
        bounds.yMax = 3;
        return bounds;
    }

    private void OnDrawElement(Rect rect, int index, bool isactive, bool isfocused)
    {
        AttackPatternData rule = tile.rawPatterns[index];
        BoundsInt bounds = GetRuleGUIBounds();

        float yPos = rect.yMin + 2f;
        float xPos = rect.xMin + 10f;
        Vector2 matrixSize = GetMatrixSize(bounds);
        Rect matrixRect = new Rect(xPos, yPos, matrixSize.x, matrixSize.y);
        DrawGrid(matrixRect, bounds, rule);
    }

    private void DrawGrid(Rect rect, BoundsInt bounds, AttackPatternData pattern)
    {
        Handles.color = EditorGUIUtility.isProSkin ? new Color(1f, 1f, 1f, 0.2f) : new Color(0f, 0f, 0f, 0.2f);
        float w = rect.width / bounds.size.x;
        float h = rect.height / bounds.size.y;

        for (int y = 0; y <= bounds.size.y; y++)
        {
            float top = rect.yMin + y * h;
            Handles.DrawLine(new Vector3(rect.xMin, top), new Vector3(rect.xMax, top));
        }
        for (int x = 0; x <= bounds.size.x; x++)
        {
            float left = rect.xMin + x * w;
            Handles.DrawLine(new Vector3(left, rect.yMin), new Vector3(left, rect.yMax));
        }
        Handles.color = Color.white;
        var targets = pattern.GetTargets();

        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                Vector3Int pos = new Vector3Int(x, y);
                Rect r = new Rect(rect.xMin + (x - bounds.xMin) * w, rect.yMin + (-y + bounds.yMax - 1) * h, w - 1, h - 1);
                DrawIcons(pattern, targets, pos, r);
            }
        }
    }

    protected virtual void DrawIcons(AttackPatternData patternData, Dictionary<Vector3Int, int> targets, Vector3Int position, Rect rect)
    {
        DrawTargets(patternData, targets, position, rect);

        if (position.x == 0 && position.y == 0 && !targets.ContainsKey(position))
        {
            GUI.DrawTexture(rect, _centerIcon);
        }
    }

    protected void DrawTargets(AttackPatternData patternData, Dictionary<Vector3Int, int> targets, Vector3Int position, Rect rect)
    {
        if (targets.ContainsKey(position))
        {
            DrawTargetIcon(rect, position, patternData);
        }
        UpdateTargets(rect, patternData, position);
    }

    private void UpdateTargets(Rect rect, AttackPatternData pattern, Vector3Int position)
    {
        if (Event.current.type == EventType.MouseDown && ContainsMousePosition(rect))
        {
            UpdatePattern(pattern, position);
            GUI.changed = true;
            Event.current.Use();
        }
    }

    protected virtual void DrawTargetIcon(Rect rect, Vector3Int position, AttackPatternData tilingRule)
    {
        GUI.DrawTexture(rect, _targetIcon);
    }

    protected virtual void UpdatePattern(AttackPatternData pattern, Vector3Int position)
    {
        pattern.UpdateTargets(position);
    }

    private Vector2 GetMatrixSize(BoundsInt bounds)
    {
        return new Vector2(bounds.size.x * SINGLE_LINE_HEIGHT, bounds.size.y * SINGLE_LINE_HEIGHT);
    }

    private bool ContainsMousePosition(Rect rect)
    {
        return rect.Contains(Event.current.mousePosition);
    }

    private float GetElementHeight(int index)
    {
        return ELEMENT_HEIGHT;
    }

}