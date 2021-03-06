using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;
using Items;
using UnityEngine.Events;

namespace Entities.InteractiveObjects
{
    public class Container : MonoBehaviour, IInteractive
    {
        public static event UnityAction<ItemSection<Item>> OnContainerOpen;
        [SerializeField] LootTable _lootTable;

        public void Interact(PlayerCore player)
        {
            var loot = new ItemSection<Item>(-1);
            _lootTable.GetLoot(ref loot);

            OnContainerOpen?.Invoke(loot);

        }
    }
}