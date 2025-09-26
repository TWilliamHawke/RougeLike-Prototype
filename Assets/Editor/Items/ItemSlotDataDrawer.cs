using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Items;

[CustomPropertyDrawer(typeof(LootItemsData))]
public class ItemSlotDataDrawer : PropertyDrawer
{
    float _fieldHeight = EditorGUIUtility.singleLineHeight;
    float _buttonWidth = 20;


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
        singleLine.y += EditorHelpers.lineHeight;
        singleLine.width -= _buttonWidth * 2;
        EditorGUI.PropertyField(singleLine, property.FindPropertyRelative("_count"));

        var buttonRect = new Rect(singleLine.x + singleLine.width, singleLine.y, _buttonWidth, _fieldHeight);
        if (GUI.Button(buttonRect, "-"))
        {
            property.FindPropertyRelative("_count").intValue--;
        }
        buttonRect.x += _buttonWidth;

        if (GUI.Button(buttonRect, "+"))
        {
            property.FindPropertyRelative("_count").intValue++;
        }


    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorHelpers.lineHeight * 2 + 5;
    }
}
