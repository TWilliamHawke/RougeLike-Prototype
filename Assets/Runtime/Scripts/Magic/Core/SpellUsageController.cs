using Effects;
using Entities.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Magic
{
    [RequireComponent(typeof(EffectsStorage))]
    [RequireComponent(typeof(StatsContainer))]
    public class SpellUsageController : MonoBehaviour
    {
        [SerializeField] ResourceChangeFactor _spellCostFactor;
        [SerializeField] StaticStat _spellPower;
        [Range(0, 1)]
        [SerializeField] float _minSpellCostOfBase = .1f;

        public event UnityAction OnMonoDestroy; 

        ResourceStorage _manaStorage;

        StatsContainer _statsContainer;
        EffectsStorage _effectsStorage;
        StaticStatStorage _spellPowerStorage;

        void Awake()
        {
            _statsContainer = GetComponent<StatsContainer>();
            _effectsStorage = GetComponent<EffectsStorage>();
            _manaStorage = _statsContainer.FindStorage(_spellCostFactor.targetResource);
            _spellPowerStorage = _statsContainer.FindStorage(_spellPower);
            _spellPowerStorage.SetNewValue(100);
        }

        void OnDestroy()
        {
            OnMonoDestroy?.Invoke();
        }

        public int GetSpellCost(KnownSpellData spellData)
        {
            int spellCost = spellData.baseManaCost;
            int minCost = Mathf.CeilToInt(_minSpellCostOfBase * spellCost);

            //UNDONE it should iterate trough all effect containers
            spellCost = _spellCostFactor.ApplyStatsToValue(spellCost, _statsContainer, spellData);
            return Mathf.Max(minCost, spellCost);
        }

        public AbilityModifiers GetSpellModifiers(KnownSpellData spellData)
        {
            int rawSpellPower = _spellPowerStorage.GetAdjustedValue(spellData);

            return new AbilityModifiers
            {
                magnitudeMult = rawSpellPower * 0.01f,
            };
        }
    }
}