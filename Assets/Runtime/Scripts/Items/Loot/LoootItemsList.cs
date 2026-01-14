using System.Collections.Generic;

namespace Items
{
    public class LoootItemsList : IDataList<ItemTemplate>
    {
        List<IDataListElement<ItemTemplate>> _itemsList = new();
        int _rarity;

        public LoootItemsList(int rarity)
        {
            _rarity = rarity;
        }

        public void AddRawLoot(IEnumerable<IDataListElement<ItemTemplate>> rawLoot)
        {
            _itemsList.AddRange(rawLoot);
        }

        public void AddElements(IDataListElement<ItemTemplate> element)
        {
            _itemsList.Add(element);
        }

        public void Clear()
        {
            _itemsList.Clear();
        }

        public void TransferItemsToSection(ILootSection section)
        {
            foreach (var itemData in _itemsList)
            {
                section.AddItems(itemData.element.CreateItem(_rarity), itemData.count);
            }
        }

    }
}