using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Entities.PlayerScripts;
using Items;
using UnityEngine;
using Entities.Behavior;
using Map.Zones;
using UnityEngine.Events;

namespace Entities.NPC
{
    public class NPC : Entity
    {
        [SerializeField] NPCSoundKit _soundKit;
        [SerializeField] CustomEvent OnLocationPanelClick;
        [SerializeField] NpcInteractionZone _interactionZone;

        NPCTemplate _template;

        public override Dictionary<DamageType, int> resists => _inventory.resists;
        //HACK 
        public override LootTable lootTable => _inventory.loot;
        public override AudioClip[] deathSounds => _soundKit.deathSounds;
        protected override ITemplateWithBaseStats template => _template;

        public MapZone interactionZone => _interactionZone;
        public override IDamageSource damageSource => throw new System.NotImplementedException();

        INPCInventory _inventory;

        public override event UnityAction<ITemplateWithBaseStats> OnTemplateApplied;

        public void BindTemplate(NPCTemplate template)
        {
            _template = template;
            _inventory = template.CreateInventory();
            InitComponents();
            ApplyStartStats(template);
            OnTemplateApplied?.Invoke(template);
        }

        public void InitInteractiveZone(IMapZoneLogic mapZoneLogic)
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
            body.PlaySound(_inventory.weapon.attackSound);
        }
    }
}


