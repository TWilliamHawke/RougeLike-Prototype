using System.Collections;
using System.Collections.Generic;
using Entities.Stats;
using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    [CustomEditor(typeof(Stat))]
    public class StatEditor : SimpleEditor
    {
        protected override void DrawProperties()
        {
            DrawPropertyField("_displayName");
            DrawPropertyField("_icon");
            DrawPropertyField("_parentStat");

            var parentStat = serializedObject.FindProperty("_parentStat");


            if (parentStat.objectReferenceValue == null)
            {
                EditorGUILayout.Space(25);
                DrawPropertyField("_capMin");
                DrawPropertyField("_capMax");
                DrawPropertyField("_defaultValue");
            }
        }
    }
}
