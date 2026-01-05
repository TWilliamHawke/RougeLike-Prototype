using UnityEditor;
using static Entities.EntitiesTable;

namespace CustomEditors
{
    [CustomPropertyDrawer(typeof(EntityData))]
    public class EntityDataDrawer : SimplePropertyDrawer
    {
    }

}