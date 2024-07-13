using Localisation;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LocalisationKeyAttribute))]
public class LocalisationKeyDrawer : PropertyDrawer
{
    float btnWidth = 23f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.String)
        {
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        string locKey = property.stringValue;
        bool isExist = TSVEditor.KeyIsExist(locKey);
        string translatedText = TSVEditor.GetEnglishText(locKey);

        float change = (attribute as PlusMinusBtnAttribute)?.amount ?? 1f;

        var propertyRect = new Rect(position.x, position.y, position.width - btnWidth * 2, position.height);

        label = new GUIContent(label.text, translatedText);
        EditorGUI.PropertyField(propertyRect, property, label);

        float buttonX = position.x + position.width - btnWidth;
        var buttonRect = new Rect(buttonX, position.y, btnWidth, position.height);

        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 16;
        var mode = isExist ? EditLocalStringWindow.WindowMode.edit : EditLocalStringWindow.WindowMode.add;

        if (GUI.Button(buttonRect, new GUIContent("\u226b", "Search"), style))
        {
        }

        buttonX -= btnWidth;

        buttonRect = new Rect(buttonX, position.y, btnWidth, position.height);

        var btnText = isExist ? "\u2713" : "X";

        string tooltip = isExist ? $"Edit text: \n\n{translatedText}" : "Add localisation key";

        if (GUI.Button(buttonRect, new GUIContent(btnText, tooltip)))
        {
            if (locKey == "") 
            {
                throw new System.Exception("Localisation key is empty");
            }
            else
            {
                EditLocalStringWindow.Open(locKey, GameLanguages.english, mode);
            }
        }

    }
}


