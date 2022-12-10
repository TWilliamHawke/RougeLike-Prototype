using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Entities;

namespace CustomEditors
{
    [CustomEditor(typeof(NPCInventory))]
    public class NPCInventoryEditor : SimpleEditor
    {
        SerializedProperty _inventoryType;
        SerializedProperty _weapon;
        SerializedProperty _freeAccessItems;
        SerializedProperty _tradeGoods;

        void OnEnable()
        {
            _inventoryType = serializedObject.FindProperty("_inventoryType");
            _weapon = serializedObject.FindProperty("_weapon");
            _freeAccessItems = serializedObject.FindProperty("_freeAccessItems");
            _tradeGoods = serializedObject.FindProperty("_tradeGoods");
        }


        protected override void DrawProperties()
        {
            EditorGUILayout.PropertyField(_inventoryType);
            EditorGUILayout.PropertyField(_weapon);
            EditorGUILayout.PropertyField(_freeAccessItems);

            var type = (target as NPCInventory)?.inventoryType;

            if (type != NPCInventoryType.normal)
            {
                EditorGUILayout.PropertyField(_tradeGoods);
            }
        }


    }
}


