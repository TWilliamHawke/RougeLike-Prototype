using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;
using Abilities;

namespace Items
{
    [CreateAssetMenu(fileName = "NewPotion", menuName = "Items/Potion")]
	public class Potion : Item, IAbilitySource, IItemWithAbility, IEffectSource
	{
		[Header("Potion Effects")]
	    [SerializeField] SourceEffectData[] _effects;

        public Sprite abilityIcon => icon;
        public bool destroyAfterUse => true;

        public IAbilityContainer CreateAbilityContainer(IAbilitiesFactory factory)
        {
            SelfAbility ability = new SelfAbility(this, _effects);
            return factory.CreateItemAbility(this, ability);
        }

        public override string GetDescription()
        {
            return "";
        }

        public override string GetItemType()
        {
            return "Potion";
        }

        public IAbility CreateAbility()
        {
            return new SelfAbility(this, _effects);
        }
    }
}