using System.Collections;
using System.Collections.Generic;
using Entities.Stats;
using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    [CustomEditor(typeof(StoredResource))]
    public class StoredResourceEditor : SimpleEditor
    {
        protected override void DrawProperties()
        {
            DrawPropertyField("_displayName");
            DrawPropertyField("_icon");
            DrawPropertyField("_useParentStat");

            var useParentStat = (target as StoredResource)?.useParentStat ?? false;

            if (useParentStat)
            {
                DrawPropertyField("_parentStat");
            }

        }


    }
}
