using UnityEngine;
using Items;

namespace Abilities
{
    public class EquipmentAbilityContainer : AbilityContainer
    {
        //UNDONE item is equipped
        public override bool canBeUsed => true; 

        Item _item;

        public override void UseAbility(AbilityController controller)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateAbilityButton(IAbilityCounterHandler handler)
        {
            throw new System.NotImplementedException();
        }
    }
}