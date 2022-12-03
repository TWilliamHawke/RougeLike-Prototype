using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;

namespace Items
{
    [CreateAssetMenu(fileName = "NewPotion", menuName = "Items/Potion")]
	public class Potion : Item, IAbilitySource, IItemWithAbility, IEffectSource, IUsable
	{
		[Header("Potion Effects")]
	    [SerializeField] SourceEffectData[] _effects;

        public Sprite abilityIcon => icon;

        public bool triggerModalWindow => false;

        public IAbilityInstruction CreateAbilityInstruction()
        {
            return new ItemUsageInstruction(this);
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