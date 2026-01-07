using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Entities.PlayerScripts;
using Items;
using UnityEngine;
using Entities.Behavior;
using Map.Zones;
using UnityEngine.Events;
using Map;

namespace Entities.NPC
{
    [RequireComponent(typeof(FactionHandler))]
    public class NPC : Entity
    {
        [SerializeField] NPCSoundKit _soundKit;
        [SerializeField] CustomEvent OnLocationPanelClick;
        [SerializeField] InteractionZone _interactionZone;

        NPCTemplate _template;
        INPCInventory _inventory;

        public override AudioClip[] deathSounds => _soundKit.deathSounds;
        public override ITemplateWithBaseStats template => _template;
        public InteractionZone interactionZone => _interactionZone;

        public override event UnityAction<ITemplateWithBaseStats> OnTemplateApplied;

        public void BindTemplate(NPCTemplate template)
        {
            _template = template;
            _inventory = template.CreateInventory();
            ApplyStartStats(template);
            OnTemplateApplied?.Invoke(template);
        }

        public override void InitInteractiveZone(IMapZone mapZoneLogic)
        {
            _interactionZone.Init(mapZoneLogic);
        }

        public override void Interact(Player player)
        {
            var behavior = GetComponent<FactionHandler>().antiPlayerBehavior;
            if (behavior == BehaviorType.agressive)
            {
                player.UseMainAbility(this);
            }
            else
            {
                OnLocationPanelClick.Invoke();
            }
        }

        public override void AddLootTo(IItemStorage storage)
        {
            _inventory.AddItemsTo(storage);
        }

        public override void RemoveLootFrom(IItemStorage storage)
        {
            _inventory.RemoveItemsFrom(storage);
        }
    }
}


