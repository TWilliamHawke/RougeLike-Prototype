using Entities.Stats;
using Items;
using UnityEngine;
using Abilities;
using Map;

namespace Magic
{
    public class SpellContainer : SpellAbilityContainer
    {
        ISafeStatController _manaStorage;
        KnownSpellData _spellData;
        MagicConfig _magicConfig;
        StatsStorage _statsStorage;
        SpellDescriptionConstructor _descriptionConstructor;

        public int spellCost => _magicConfig.GetSpellCost(_spellData, _statsStorage);
        public override bool canBeUsed => _manaStorage.currentValue >= spellCost;
        public override string displayName => _spellData.displayName;
        public override Sprite icon => _spellData.icon;
        public KnownSpellData spellData => _spellData;
        protected override IAbility ability => _spellData.spellEffect;

        public SpellContainer(KnownSpellData spellData, IAbilityUser user, MagicConfig magicConfig)
        {
            _spellData = spellData;
            _magicConfig = magicConfig;
            _statsStorage = user.GetEntityComponent<StatsStorage>();
            _manaStorage = magicConfig.FindManaStorage(_statsStorage);
            _descriptionConstructor = new(spellData, _statsStorage, _magicConfig);
        }

        public override void UseAbility(IAbilityTarget target)
        {
            if (_manaStorage.TryReduceStat(spellCost))
            {
                ability.Use(target);
            }
        }

        public bool HasSpell(KnownSpellData spell)
        {
            return _spellData == spell;
        }

        public string ConstructDescription()
        {
            return _descriptionConstructor.ConstructDescription();
        }

        public string GetRankUpDescription()
        {
            return _descriptionConstructor.GetRankUpDescription();
        }

        public string ConstructDescriptionWith(int slotIndex, SpellString spellString)
        {
            return _descriptionConstructor.ConstructDescriptionWith(slotIndex, spellString);
        }

        public string GetRankUpSpellCost()
        {
            return _descriptionConstructor.GetRankUpSpellCost();
        }

        public string GetSpellCostWith(int slotIndex, SpellString spellString)
        {
            return _descriptionConstructor.GetSpellCostWith(slotIndex, spellString);
        }

        public override bool TileHasValidTarget(ITileClickData tile)
        {
            return ability.TileHasValidTarget(tile);
        }
    }
}