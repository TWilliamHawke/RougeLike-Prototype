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
        protected override string[] _properties => properties;

        string[] properties = { "stat", "value"};

    }
}
