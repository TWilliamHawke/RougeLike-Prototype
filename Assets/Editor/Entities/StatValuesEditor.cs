using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Stats;
using UnityEditor;

namespace CustomEditors
{
    [CustomEditor(typeof(StatValues))]
    public class StatValuesEditor : SimpleEditor
    {
        		protected override void DrawProperties()
		{
            DrawPropertyField("_statValues");
        }
    }

}
