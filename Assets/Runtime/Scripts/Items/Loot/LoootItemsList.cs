using System.Collections.Generic;

namespace Items
{
    public class LoootItemsList : IDataList<ItemTemplate>
    {
        List<IDataCount<ItemTemplate>> _itemsList = new();
        ILootStorage _storage;

        public LoootItemsList(ILootStorage storage)
        {
            _storage = storage;
        }

        public void AddElements(IDataCount<ItemTemplate> element)
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