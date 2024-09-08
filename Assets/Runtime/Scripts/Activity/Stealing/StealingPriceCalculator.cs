using System.Collections;
using System.Collections.Generic;
using Entities.NPC;
using Entities.PlayerScripts;
using UnityEngine;

namespace Items
{
    public class StealingCostCalculator : IItemPriceCalculator
    {
        PlayerStats _playerStats { get; init; }

        public StealingCostCalculator(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        public void SetPrices(IContainersList inventory)
        {
            for (int i = 0; i < inventory.count; i++)
            {
                var container = inventory.ContainerAt(i);
                foreach (var item in container)
                {
                    SetItemPrice(container, item);
                }
            }
        }

        private void SetItemPrice(IItemContainer storage, ItemSlotData item)
        {
            int price = 40;

            if (storage.storageType == ItemStorageType.inventory)
            {
                price *= 2;
            }

            item.slotPrice = price;
        }
    }
}
