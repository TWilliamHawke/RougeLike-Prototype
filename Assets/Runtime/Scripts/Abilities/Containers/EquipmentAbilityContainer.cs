using UnityEngine;
using Items;

namespace Abilities
{
    public class EquipmentAbilityContainer : IAbilityContainer
    {
        //UNDONE item is equipped
        public bool canBeUsed => throw new System.NotImplementedException(); 
        public int numOfUses => -1;
        public string displayName => _item.displayName;
        public Sprite icon => _item.icon;

        Item _item;

        public void UseAbility(AbilityController controller)
        {
            throw new System.NotImplementedException();
        }
    }
}