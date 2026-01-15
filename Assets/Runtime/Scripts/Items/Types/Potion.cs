using System.Collections.Generic;
using Abilities;
using Effects;

namespace Items
{
    public class Potion : StaticItem, IAbilitySource, IEffectSource
    {
        public override int value => _potionTemplate.value;

        protected override StaticItemTemplate _staticTemplate => _potionTemplate;

        PotionTemplate _potionTemplate;

        public Potion(PotionTemplate potionTemplate)
        {
            _potionTemplate = potionTemplate;
        }

        public IEnumerable<ISourceEffectData> GetEffects()
        {
            return _potionTemplate.GetEffects();
        }

        public IAbilityContainer CreateAbilityContainer(IAbilitiesFactory factory)
        {
            var ability = CreateAbility();
            return factory.CreateItemContainer(this, ability);
        }

        private IAbility CreateAbility()
        {
            SelfAbility ability = new(_potionTemplate);
            ability.BindEffectSource(this);
            _potionTemplate.BindController(ability);
            return ability;
        }

        public override string GetDescription()
        {
            return _potionTemplate.GetDescription();
        }
    }
}