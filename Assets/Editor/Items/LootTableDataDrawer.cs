using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Items;

[CustomPropertyDrawer(typeof(LootTableData))]
public class LootTableDataDrawer : PropertyDrawer
{
    float _fieldHeight = EditorGUIUtility.singleLineHeight;
    float _buttonWidth = 20;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var singleLine = EditorHelpers.RectToSingleLine(position);

        EditorGUI.PropertyField(singleLine, property.FindPropertyRelative("lootTable"));
        singleLine.y += EditorHelpers.lineHeight;
        singleLine.width -= _buttonWidth * 2;
        EditorGUI.PropertyField(singleLine, property.FindPropertyRelative("chanceOfNone"));

        var buttonRect = new Rect(singleLine.x + singleLine.width, singleLine.y, _buttonWidth, _fieldHeight);
        if (GUI.Button(buttonRect, "-"))
        {
            property.FindPropertyRelative("chanceOfNone").floatValue -= 0.1f;
        }
        buttonRect.x += _buttonWidth;

        if (GUI.Button(buttonRect, "+"))
        {
            property.FindPropertyRelative("chanceOfNone").floatValue += 0.1f;
        }


    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorHelpers.lineHeight * 2 + 10;
    }
}
