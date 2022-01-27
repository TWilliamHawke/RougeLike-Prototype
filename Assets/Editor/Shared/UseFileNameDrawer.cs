using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

[CustomPropertyDrawer(typeof(UseFileNameAttribute))]
public class UseFileNameDrawer : PropertyDrawer
{
    float btnWidth = 23f;

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
                string pattern = @"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])";
                Regex regex = new Regex(pattern);

                property.stringValue = regex.Replace(fileName, " "); ;
            }
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }


    }

}
