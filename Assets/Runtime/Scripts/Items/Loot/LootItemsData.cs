using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class LootItemsData : IDataCount<ItemTemplate>
    {
        [SerializeField] ItemTemplate _item;
        [SerializeField] int _count;

        public int count => _count;
        public ItemTemplate element => _item;

        public LootItemsData(ItemTemplate item, int count)
        {
            _item = item;
            _count = count;
        }
    }
}