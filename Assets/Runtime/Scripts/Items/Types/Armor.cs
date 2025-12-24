using Items.Equipment;

namespace Items
{
    public class Armor : AbstractItem, IEquipment
    {
        protected override ItemTemplate _template => _armorTemplate;
        public override int value => _armorTemplate.CalcValue(_armorQualityData);
        public override string displayName => _armorTemplate.ConstructName(_armorQualityData);

        public EquipmentSlotTemplate equipmentSlot => _armorTemplate.equipmentSlot;

        ArmorTemplate _armorTemplate;
        ArmorQualityData _armorQualityData;

        public Armor(ArmorTemplate armorTemplate, ArmorQualityData armorQualityData)
        {
            _armorTemplate = armorTemplate;
            _armorQualityData = armorQualityData;
        }

        public override string GetDescription()
        {
            throw new System.NotImplementedException();
        }
    }
}