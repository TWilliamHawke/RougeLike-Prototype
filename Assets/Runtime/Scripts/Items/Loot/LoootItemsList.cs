using System.Collections.Generic;

namespace Items
{
    public class LoootItemsList : IDataList<ItemTemplate>
    {
        List<IDataListElement<ItemTemplate>> _itemsList = new();
        ILootSection _storage;

        public LoootItemsList(ILootSection storage)
        {
            _storage = storage;
        }

        public void AddElements(IDataListElement<ItemTemplate> element)
        {
            _itemsList.Add(element);
        }

        public void CreateItems(int rarity = 0)
        {
            foreach (var itemData in _itemsList)
            {
                _storage.AddItems(itemData.element.CreateItem(rarity), itemData.count);
            }

            _itemsList.Clear();
        }
    }
}