using System.Collections;
using System.Collections.Generic;
using Entities.Behavior;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class FactionHandler : MonoBehaviour, IFactionMember
    {
        [SerializeField] Faction _faction;
        public Faction faction => _faction;
        public BehaviorType antiPlayerBehavior => faction?.GetAntiPlayerBehavior() ?? BehaviorType.neutral;

        public BehaviorType behavior => _faction.GetAntiPlayerBehavior();

        public event UnityAction<Faction> OnFactionChange;

        void Awake()
        {
            if (TryGetComponent<IEntityWithTemplate>(out var entity))
            {
                entity.OnTemplateApplied += ApplyDefaultFaction;
            }
        }

        public void ReplaceFaction(Faction newFaction)
        {
            _faction = newFaction;
            OnFactionChange?.Invoke(_faction);
        }

        private void ApplyDefaultFaction(ITemplateWithBaseStats template)
        {
            ReplaceFaction(template.faction);
        }
    }
}
