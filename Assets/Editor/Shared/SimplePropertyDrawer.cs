using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

namespace CustomEditors
{
    public abstract class SimplePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            FieldInfo[] fields = GetFieldsFromPropertyDrawer();

            var singleLine = EditorHelpers.RectToSingleLine(position);

            foreach (var field in fields)
            {
                EditorGUI.PropertyField(singleLine, property.FindPropertyRelative(field.Name));
                singleLine.y += EditorHelpers.lineHeight;
            }
        }

        private FieldInfo[] GetFieldsFromPropertyDrawer()
        {
            Type type = GetTypeFromPropertyDrawer();

            return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.DeclaredOnly | BindingFlags.Instance);
        }

        private Type GetTypeFromPropertyDrawer()
        {
            var attr = this.GetType().GetCustomAttributes(true)[0] as CustomPropertyDrawer;
            var prop1 = typeof(CustomPropertyDrawer).GetField("m_Type", BindingFlags.NonPublic | BindingFlags.Instance);
            var type = prop1.GetValue(attr) as Type;
            return type;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var fieldsCount = GetFieldsFromPropertyDrawer().Length;
            return EditorHelpers.lineHeight * fieldsCount + 5;
        }
    }

}


