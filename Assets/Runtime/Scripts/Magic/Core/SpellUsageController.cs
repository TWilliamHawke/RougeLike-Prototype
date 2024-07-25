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
        [Range(0, 1)]
        [SerializeField] float _minSpellCostOfBase = .1f;

        public event UnityAction OnMonoDestroy; 

        ResourceStorage _manaStorage;

        StatsContainer _statsContainer;
        EffectsStorage _effectsStorage;

        void Awake()
        {
            _statsContainer = GetComponent<StatsContainer>();
            _effectsStorage = GetComponent<EffectsStorage>();
            _manaStorage = _statsContainer.FindStorage(_spellCostFactor.targetResource);
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
    }
}