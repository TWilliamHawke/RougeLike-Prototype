namespace Items
{
    public class NPCEquipment : ItemContainer, IEquipmentStorage
    {
        public NPCEquipment(ItemSection section) : base(section)
        {
            
        }

        public ItemSlotData GetEquipment(EquipmentTypes type)
        {
            throw new System.NotImplementedException();
        }

        public bool HasEquipment(EquipmentTypes type)
        {
            throw new System.NotImplementedException();
        }
    }
}
