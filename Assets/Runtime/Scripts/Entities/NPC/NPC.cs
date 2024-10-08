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
    public class NPC : Entity
    {
        [SerializeField] NPCSoundKit _soundKit;
        [SerializeField] CustomEvent OnLocationPanelClick;
        [SerializeField] InteractionZone _interactionZone;

        NPCTemplate _template;

        public override Dictionary<DamageType, int> resists => inventory.resists;
        //HACK 
        public override LootTable lootTable => inventory.loot;
        public override AudioClip[] deathSounds => _soundKit.deathSounds;
        public override ITemplateWithBaseStats template => _template;

        public InteractionZone interactionZone => _interactionZone;
        public override IDamageSource damageSource => throw new System.NotImplementedException();

        public INPCInventory inventory { get; private set;}

        public override event UnityAction<ITemplateWithBaseStats> OnTemplateApplied;

        public void BindTemplate(NPCTemplate template)
        {
            _template = template;
            inventory = template.CreateInventory();
            InitComponents();
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
                player.Attack(this);
            }
            else
            {
                OnLocationPanelClick.Invoke();
            }
        }

        public override void PlayAttackSound()
        {
            body.PlaySound(inventory.weapon.attackSound);
        }

        public override void AddLoot(IItemStorage storage)
        {
            inventory.ForEach(container => storage.AddItems(container));
        }

        public override void RemoveLoot(IItemStorage storage)
        {
            inventory.ForEach(container => storage.RemoveItems(container));
        }
    }
}


