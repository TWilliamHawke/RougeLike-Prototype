using UnityEngine;
using Items;
using Map;

namespace Abilities
{
    public class EquipmentAbilityContainer : AbilityContainer
    {
        //UNDONE item is equipped
        public override bool canBeUsed => true; 
        protected override IAbility ability => _ability;

        IAbility _ability { get; init; }
        Item _item;

        public override void UseAbility(IAbilityTarget target)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateAbilityButton(IAbilityCounterHandler handler)
        {
            throw new System.NotImplementedException();
        }

        public override bool TileHasValidTarget(ITileClickData tile)
        {
            throw new System.NotImplementedException();
        }
    }
}