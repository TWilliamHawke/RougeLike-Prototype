using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public static class EditorHelpers
{
    public static float lineHeight => EditorGUIUtility.singleLineHeight + 2;

    public static string Capitalize(string str)
    {
        return str switch
        {
            null => "",
            "" => "",
            _ => str[0].ToString().ToUpper() + str.Substring(1)
        };
    }

    public static Rect RectToSingleLine(Rect position)
    {
        return new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
    }

    public static void NextLine(ref Rect position)
    {
        position.y += lineHeight;
    }

    public static Rect CutX(Rect position, float start, float end)
    {
        return new Rect(position.x + start, position.y, end - start, EditorGUIUtility.singleLineHeight);
    }

    public static Rect CutX(Rect position, float start)
    {
        if (start < 0)
        {
            return new Rect(position.x + position.width + start, position.y, -start, EditorGUIUtility.singleLineHeight);
        }


        return new Rect(position.x + start, position.y, position.width - start, EditorGUIUtility.singleLineHeight);
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
