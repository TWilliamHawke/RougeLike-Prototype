using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System;

[CustomPropertyDrawer(typeof(UseFileNameAttribute))]
public class UseFileNameDrawer : PropertyDrawer
{
    float btnWidth = 23f;
    const string UPPERCASE_PATTERN = @"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])";
    static readonly Regex regex = new Regex(UPPERCASE_PATTERN);

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.type == "string")
        {
            var propertyRect = new Rect(position.x, position.y, position.width - btnWidth, position.height);
            EditorGUI.PropertyField(propertyRect, property, label);

            float buttonX = position.x + position.width - btnWidth;
            var buttonRect = new Rect(buttonX, position.y, btnWidth, position.height);

            if (GUI.Button(buttonRect, new GUIContent("fn", "Replace text with filename")))
            {
                string fileName = property.serializedObject.targetObject.name;
                //split camelcase into separate words
                var splittedName = regex.Replace(fileName, " ");
                property.stringValue = EditorHelpers.Capitalize(splittedName);
            }
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }


    }

}
