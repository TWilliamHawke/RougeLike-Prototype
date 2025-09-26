using System.Collections.Generic;

namespace Items
{
    public class LoootItemsList : IDataList<ItemTemplate>
    {
        List<ItemTemplate> _itemsList;
        ILootStorage _storage;

        public LoootItemsList(ILootStorage storage)
        {
            _storage = storage;
        }

        public void AddElements(ItemTemplate item, int count)
        {
            for (int i = 0; i < count; i++)
            {
                _itemsList.Add(item);
            }
        }

        public void Flush()
        {
            foreach (var item in _itemsList)
            {
                _storage.AddItem(item);
            }

            _itemsList.Clear();
        }
    }
}