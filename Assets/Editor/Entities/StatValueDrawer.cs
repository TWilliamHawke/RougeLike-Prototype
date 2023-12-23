using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Entities.Stats;


namespace CustomEditors
{
    [CustomPropertyDrawer(typeof(StatValue))]
    public class StatValueDrawer : SimplePropertyDrawer
    {
    }
}
