using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
			
			if(GUI.Button(buttonRect, new GUIContent("fn", "Replace text with filename")))
			{
				property.stringValue = property.serializedObject.targetObject.name;
			}
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }


    }

}
