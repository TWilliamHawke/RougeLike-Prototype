using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace CustomEditors
{
    [CustomPropertyDrawer(typeof(PlusMinusBtnAttribute))]
    public class PlusMinusBtnDrawer : PropertyDrawer
    {
        float btnWidth = 23f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Integer || property.propertyType == SerializedPropertyType.Float)
            {
                float change = (attribute as PlusMinusBtnAttribute)?.amount ?? 1f;

                var propertyRect = new Rect(position.x, position.y, position.width - btnWidth * 2, position.height);
                EditorGUI.PropertyField(propertyRect, property, label);

                float buttonX = position.x + position.width - btnWidth;
                var buttonRect = new Rect(buttonX, position.y, btnWidth, position.height);

                if (GUI.Button(buttonRect, new GUIContent("-", $"Reduce Value by {change}")))
                {
                    property.boxedValue = property.intValue - change;
                }

                buttonX -= btnWidth;

                buttonRect = new Rect(buttonX, position.y, btnWidth, position.height);

                if (GUI.Button(buttonRect, new GUIContent("+", $"Increase Value by {change}")))
                {
                    Debug.Log(property.intValue + change);
                    property.boxedValue = property.intValue + change;
                }

            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }


        }
    }
}
