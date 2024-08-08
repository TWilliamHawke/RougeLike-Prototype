using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    [CustomPropertyDrawer(typeof(LocalString))]
    public class LocalStringDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var locKey = property.FindPropertyRelative("_locKey");

            LocalisationKeyDrawer.DrawKey(position, locKey, ref label);
        }
    }
}
