using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Items
{
    public class ItemSlotWithPrice : ItemSlot
    {
        [SerializeField] EdgeLabel _price;

        public override void BindData(ItemSlotData slotData)
        {
            if (slotData.item is null) return;
            base.BindData(slotData);
            _price.SetEdge(slotData.slotPrice);
            _price.gameObject.SetActive(true);
        }

        public void SetValueStorage(IValueStorage valueStorage)
        {
            _price.AddToObserve(valueStorage);
        }
    }
}