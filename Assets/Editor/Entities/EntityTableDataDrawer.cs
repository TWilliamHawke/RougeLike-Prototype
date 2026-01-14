using UnityEditor;
using static Entities.EntitiesTable;

namespace CustomEditors
{
    [CustomPropertyDrawer(typeof(EntityTableData))]
    public class EntityTableDataDrawer : SimplePropertyDrawer
    {
    }

}