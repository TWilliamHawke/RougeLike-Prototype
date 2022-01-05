using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;

namespace Items
{
    [CreateAssetMenu(fileName = "NewPotion", menuName = "Items/Potion")]
	public class Potion : Item, IAbilitySource, IItemWithAbility
	{
		[Header("Potion Effects")]
	    [SerializeField] SourceEffectData[] _effects;

        public Sprite abilityIcon => icon;

        public IAbilityInstruction CreateAbilityInstruction()
        {
            return new ItemContainer(this);
        }

        public void UseAbility(AbilityController controller)
        {
            foreach (var effectData in _effects)
			{
				controller.ApplyToSelf(effectData);
			}
        }


    }
}