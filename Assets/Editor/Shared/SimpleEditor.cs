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

        protected virtual void DrawProperties()
        {
        }
    }
}


