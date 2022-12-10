using UnityEngine;
using UnityEditor;

namespace CustomEditors
{
    public abstract class SimplePropertyDrawer : PropertyDrawer
    {
        abstract protected string[] _properties { get; }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var singleLine = EditorHelpers.RectToSingleLine(position);

            foreach (var prop in _properties)
            {
                EditorGUI.PropertyField(singleLine, property.FindPropertyRelative(prop));
                singleLine.y += EditorHelpers.lineHeight;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorHelpers.lineHeight * _properties.Length + 5;
        }
    }

}


