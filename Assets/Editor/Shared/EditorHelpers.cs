using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public static class EditorHelpers
{
    public static float lineHeight => EditorGUIUtility.singleLineHeight + 2;

    public static Rect RectToSingleLine(Rect position)
    {
        return new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
    }

    /// <summary>
    /// Get all instances of scriptable objects with given type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetAllInstances<T>() where T : ScriptableObject
    {
        return AssetDatabase.FindAssets($"t: {typeof(T).Name}")
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<T>)
                    .ToList();
    }

    public static GUIStyle HeaderStyle()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 18;
        style.richText = true;
        return style;
    }
}
