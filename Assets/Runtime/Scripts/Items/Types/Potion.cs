using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;
using Abilities;

namespace Items
{
    [CreateAssetMenu(fileName = "NewPotion", menuName = "Items/Potion")]
	public class Potion : Item, IAbilitySource, IItemWithAbility, IEffectSource, IUsableItem
	{
		[Header("Potion Effects")]
	    [SerializeField] SourceEffectData[] _effects;

        public Sprite abilityIcon => icon;

        public bool triggerModalWindow => false;
        public bool destroyAfterUse => true;

        public IAbilityContainer CreateAbilityInstruction(AbilitiesFactory factory)
        {
            return factory.CreateItemAbility(this);
        }

        public void UseItem(AbilityController controller)
        {
            UseAbility(controller);
        }

        public void UseAbility(AbilityController controller)
        {
            foreach (var effectData in _effects)
			{
				controller.ApplyToSelf(effectData, this);
			}
        }

        public override string GetDescription()
        {
            return "";
        }

        public override string GetItemType()
        {
            return "Potion";
        }
    }
}