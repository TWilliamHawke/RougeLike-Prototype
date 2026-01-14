using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Items;

[CustomPropertyDrawer(typeof(LootTable.LootItemsData))]
public class ItemSlotDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var singleLine = EditorHelpers.RectToSingleLine(position);
        singleLine.x += 26;
        singleLine.width -= 26;
        var item = property.FindPropertyRelative("_item");

        var icon = (item.objectReferenceValue as IIconData)?.icon;

        var iconRect = new Rect(position.x - 12, position.y, 36, 36);
        if (icon != null)
        {
            EditorGUI.LabelField(iconRect, new GUIContent(icon.texture));
        }
        else
        {
            EditorGUI.LabelField(iconRect, "", GUI.skin.box);
        }

        EditorGUI.PropertyField(singleLine, item);
        DrawProperty(property, ref singleLine, "_count");
        DrawProperty(property, ref singleLine, "_weight");

    }

    private void DrawProperty(SerializedProperty property, ref Rect singleLine, string propertyName)
    {
        singleLine.y += EditorHelpers.lineHeight;
        EditorGUI.PropertyField(singleLine, property.FindPropertyRelative(propertyName));
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorHelpers.lineHeight * 3 + 5;
    }
}
