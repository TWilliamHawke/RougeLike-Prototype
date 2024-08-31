using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HideIfAttribute))]
public class HideIfDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (ShouldHide(property))
        {
            return;
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (ShouldHide(property))
        {
            return 0;
        }
        else
        {
            return base.GetPropertyHeight(property, label);
        }
    }

    private bool ShouldHide(SerializedProperty property)
    {
        var hideIf = attribute as HideIfAttribute;
        if (hideIf == null) return false;

        var field = property.serializedObject.FindProperty(hideIf.fieldName);
        if (field == null) return false;

        var fieldValue = field.boxedValue;

        return fieldValue.Equals(hideIf.fieldValue);
    }
}


