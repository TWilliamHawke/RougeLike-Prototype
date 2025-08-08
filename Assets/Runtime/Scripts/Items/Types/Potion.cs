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
            SelfAbility ability = new SelfAbility(this, _effects, factory.abilityUser);
            return factory.CreateItemAbilityContainer(this, ability);
        }

        public override string GetDescription()
        {
            return "Potion description";
        }

        public override string GetItemType()
        {
            return "Potion";
        }
    }
}