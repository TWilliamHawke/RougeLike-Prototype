using System.Collections.Generic;
using UnityEngine;
using Effects;
using Abilities;

namespace Items
{
    [CreateAssetMenu(fileName = "NewPotion", menuName = "Items/Potion")]
    public class PotionTemplate : StaticItemTemplate, IEffectSource
    {
        [SerializeField] Injector _selfAbilityController;
        [Header("Potion Effects")]
        [SerializeField] SourceEffectData[] _effects;

        public Sprite abilityIcon => icon;
        public bool destroyAfterUse => true;

        public override IItem CreateItem(int rarity = 0)
        {
            return new Potion(this);
        }

        public override string GetDescription()
        {
            return "Potion description";
        }

        public IEnumerable<ISourceEffectData> GetEffects()
        {
            return _effects;
        }

        public void BindController(SelfAbility ability)
        {
            _selfAbilityController.AddInjectionTarget(ability);
        }
    }
}