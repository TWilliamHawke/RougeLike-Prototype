using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    public class SimpleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.Update();
            
            DrawScriptName();
            DrawProperties();

            serializedObject.ApplyModifiedProperties();
            EditorGUI.EndChangeCheck();
        }

        private void DrawScriptName()
        {
            GUI.enabled = false;
            var property = serializedObject.GetIterator();
            property.NextVisible(true);
            EditorGUILayout.PropertyField(property);
            GUI.enabled = true;
        }

        protected void DrawPropertyField(string propertyName)
        {
            var property = serializedObject.FindProperty(propertyName);
            EditorGUILayout.PropertyField(property);
        }

        protected virtual void DrawProperties()
        {
        }
    }
}


