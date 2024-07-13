using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    [CustomPropertyDrawer(typeof(IntValue))]
    public class IntValueDrawer : PropertyDrawer
    {
        float _lineHeight = EditorGUIUtility.singleLineHeight + 2;
        float toggleWidth = 20f;
        static float[] values = new float[2];
        static GUIContent[] s_XYLabels = new GUIContent[2]
        {
            new GUIContent("Min"),
            new GUIContent("Max")
        };


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var minValueField = property.FindPropertyRelative("_minValue");
            var maxValueField = property.FindPropertyRelative("_maxValue");
            var isRandomField = property.FindPropertyRelative("_isRandom");

            var fullLine = EditorHelpers.RectToSingleLine(position);
            var restRect = EditorGUI.PrefixLabel(fullLine, label);
            EditorGUI.BeginChangeCheck();

            int minValue = minValueField.intValue;
            int maxValue = maxValueField.intValue;
            bool isRandom = isRandomField.boolValue;

            if (isRandomField.boolValue)
            {
                var halfSize = (restRect.width - toggleWidth) * .5f;
                restRect.width = halfSize - 2;
                float labelWidth = EditorGUIUtility.labelWidth;

                EditorGUIUtility.labelWidth = 25;
                minValue = EditorGUI.IntField(restRect, "Min", minValue);
                restRect.x += halfSize;
                maxValue = EditorGUI.IntField(restRect, "Max", maxValue);
                EditorGUIUtility.labelWidth = labelWidth;
            }
            else
            {
                var rectWoToggle = EditorHelpers.CutX(restRect, 0, restRect.width - toggleWidth);
                minValue = EditorGUI.IntField(rectWoToggle, maxValue);
                maxValue = minValue;
            }

            var toggle = EditorHelpers.CutX(fullLine, -toggleWidth);
            toggle.x += 2;
            isRandom = EditorGUI.Toggle(toggle, isRandom);

            if (EditorGUI.EndChangeCheck())
            {
                minValueField.intValue = minValue;
                maxValueField.intValue = maxValue;
                isRandomField.boolValue = isRandom;
            }
        }
    }
}
