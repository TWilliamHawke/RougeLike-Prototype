using Entities;
using Entities.NPCScripts;
using UnityEditor;

namespace CustomEditors
{
    [CustomPropertyDrawer(typeof(TraderContainerTemplate))]
    public class TraderContainerDrawer : SimplePropertyDrawer
    {
        string[] properties = { "containerName", "security", "loot" };
        protected override string[] _properties => properties;
    }

}