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

        public override AudioClip[] deathSounds => _soundKit.deathSounds;
        public override ITemplateWithBaseStats template => _template;

        public InteractionZone interactionZone => _interactionZone;

        public INPCInventory inventory { get; private set;}

        public override event UnityAction<ITemplateWithBaseStats> OnTemplateApplied;

        public void BindTemplate(NPCTemplate template)
        {
            _template = template;
            inventory = template.CreateInventory();
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
            inventory.ForEach(container => storage.AddItemsFrom(container));
        }

        public override void RemoveLootFrom(IItemStorage storage)
        {
            inventory.ForEach(container => storage.RemoveItems(container));
        }
    }
}


