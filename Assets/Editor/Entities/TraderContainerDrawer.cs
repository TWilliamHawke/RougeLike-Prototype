using Entities;
using UnityEditor;

namespace CustomEditors
{
    [CustomPropertyDrawer(typeof(TraderContainer))]
    public class TraderContainerDrawer : SimplePropertyDrawer
    {
        string[] properties = { "containerName", "security", "loot" };
        protected override string[] _properties => properties;
    }

}