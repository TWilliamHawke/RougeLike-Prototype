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
            if (PropertyIsNumber(property))
            {
                DrawButtons(position, property, label);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        private void DrawButtons(Rect position, SerializedProperty property, GUIContent label)
        {
            float change = (attribute as PlusMinusBtnAttribute)?.amount ?? 1f;

            var propertyRect = new Rect(position.x, position.y, position.width - btnWidth * 2, position.height);
            EditorGUI.PropertyField(propertyRect, property, label);

            float buttonX = position.x + position.width - btnWidth;
            var buttonRect = new Rect(buttonX, position.y, btnWidth, position.height);

            if (GUI.Button(buttonRect, new GUIContent("-", $"Reduce Value by {change}")))
            {
                ChangeValue(property, -change);
            }

            buttonX -= btnWidth;

            buttonRect = new Rect(buttonX, position.y, btnWidth, position.height);

            if (GUI.Button(buttonRect, new GUIContent("+", $"Increase Value by {change}")))
            {
                ChangeValue(property, change);
            }
        }

        private bool PropertyIsNumber(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.Integer)
            {
                return true;
            }
            if (fieldInfo.FieldType == typeof(IntValue))
            {
                return true;
            }
            if (property.propertyType == SerializedPropertyType.Float)
            {
                return true;
            }
            return false;
        }

        private void ChangeValue(SerializedProperty property, float change)
        {
            if (fieldInfo.FieldType == typeof(IntValue))
            {
                var minValueField = property.FindPropertyRelative("_minValue");
                var maxValueField = property.FindPropertyRelative("_maxValue");
                minValueField.intValue += (int)change;
                maxValueField.intValue += (int)change;
                return;
            }
            property.boxedValue = property.intValue + change;
        }
    }
}
