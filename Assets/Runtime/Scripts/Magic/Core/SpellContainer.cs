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
        StatsStorage _statsContainer;
        SpellDescriptionConstructor _descriptionConstructor;
        IAbilityUser _user;

        public int spellCost => _magicConfig.GetSpellCost(_spellData, _statsContainer);
        public override bool canBeUsed => _manaStorage.currentValue >= spellCost;
        public override string displayName => _spellData.displayName;
        public override Sprite icon => _spellData.icon;
        public KnownSpellData spellData => _spellData;
        protected override IAbility ability => _spellData.spellEffect;

        public SpellContainer(KnownSpellData spellData, IAbilityUser user, MagicConfig magicConfig)
        {
            _user = user;
            _spellData = spellData;
            _magicConfig = magicConfig;
            _statsContainer = user.GetEntityComponent<StatsStorage>();
            _manaStorage = magicConfig.FindManaStorage(_statsContainer);
            _descriptionConstructor = new(spellData, _statsContainer, _magicConfig);
        }

        public override void UseAbility(IAbilityTarget target)
        {
            if (_manaStorage.TryReduceStat(spellCost))
            {
                _spellData.spellEffect.Use(_user, target);
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

        public string ConstructDescriptionWith(int slotIndex, SpellStringTemplate spellString)
        {
            return _descriptionConstructor.ConstructDescriptionWith(slotIndex, spellString);
        }

        public string GetRankUpSpellCost()
        {
            return _descriptionConstructor.GetRankUpSpellCost();
        }

        public string GetSpellCostWith(int slotIndex, SpellStringTemplate spellString)
        {
            return _descriptionConstructor.GetSpellCostWith(slotIndex, spellString);
        }

        public override bool TileHasValidTarget(ITileClickData tile)
        {
            return ability.TileHasValidTarget(_user, tile);
        }
    }
}