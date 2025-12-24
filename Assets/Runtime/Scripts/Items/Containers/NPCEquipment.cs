using Items.Equipment;

namespace Items
{
    public class NPCEquipment : ItemContainer, IEquipmentStorage
    {
        public NPCEquipment(ItemSection section) : base(section)
        {
            
        }

        public ItemSlotData GetEquipment(IEquipmentSlotTemplate slot)
        {
            throw new System.NotImplementedException();
        }

        public bool HasEquipment(IEquipmentSlotTemplate slot)
        {
            throw new System.NotImplementedException();
        }
    }
}
