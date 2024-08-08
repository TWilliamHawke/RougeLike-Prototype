using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Items
{
    public class WideItemSlot : ItemSlot
    {
        [SerializeField] TextMeshProUGUI _itemName;

        public override void BindData(ItemSlotData slotData)
        {
            base.BindData(slotData);
            _itemName.text = slotData.item.name;
        }

    }
}