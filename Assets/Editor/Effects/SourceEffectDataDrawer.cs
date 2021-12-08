using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(SourceEffectData))]
public class SourceEffectDataDrawer : PropertyDrawer
{
    float _lineHeight = EditorGUIUtility.singleLineHeight + 2;
    float _fieldHeight = EditorGUIUtility.singleLineHeight;
    float _buttonWidth = 20;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Debug.Log("work");
        var singleLine = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(singleLine, property.FindPropertyRelative("_effect"));
        singleLine.y += _lineHeight;
        EditorGUI.PropertyField(singleLine, property.FindPropertyRelative("_power"));
        singleLine.y += _lineHeight;
        singleLine.width -= _buttonWidth * 2;
        EditorGUI.PropertyField(singleLine, property.FindPropertyRelative("_duration"));

        var buttonRect = new Rect(singleLine.x + singleLine.width, singleLine.y, _buttonWidth, _fieldHeight);
        if (GUI.Button(buttonRect, "-"))
        {
            property.FindPropertyRelative("_duration").intValue--;
        }
        buttonRect.x += _buttonWidth;

        if (GUI.Button(buttonRect, "+"))
        {
            property.FindPropertyRelative("_duration").intValue++;
        }


    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return _lineHeight * 3 + 5;
    }

}
