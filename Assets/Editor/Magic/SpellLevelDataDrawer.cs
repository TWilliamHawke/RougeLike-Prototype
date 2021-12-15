using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Magic;

namespace CustomEditors
{
	[CustomPropertyDrawer(typeof(SpellLevelData))]
    public class SpellLevelDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var singleLine = EditorHelpers.RectToSingleLine(position);

            EditorGUI.PropertyField(singleLine, property.FindPropertyRelative("spellEffect"));
            singleLine.y += EditorHelpers.lineHeight;
            EditorGUI.PropertyField(singleLine, property.FindPropertyRelative("manaCost"));

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorHelpers.lineHeight * 2 + 5;
        }

    }
}