using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Entities.PlayerScripts;
using Items;
using UnityEngine;
using Entities.Behavior;

namespace Entities.NPCScripts
{
    public class NPC : Entity
    {
        [SerializeField] NPCSoundKit _soundKit;
        [SerializeField] CustomEvent OnLocationPanelClick;

        NPCTemplate _template;
        

        public override Dictionary<DamageType, int> resists => _inventory.resists;
        //HACK 
        public override LootTable lootTable => _template.inventory.freeAccessItems;
        public override AudioClip[] deathSounds => _soundKit.deathSounds;
        protected override EntityTemplate template => _template;

		NPCInventory _inventory;

        public void BindTemplate(NPCTemplate template)
        {
            _template = template;
			_inventory = new NPCInventory(template.inventory);
            InitComponents();
            ApplyStartStats(template);
        }

        public override void Interact(Player player)
        {
            if (antiPlayerBehavior == BehaviorType.agressive)
            {
                player.Attack(this);
            }
            else
            {
                OnLocationPanelClick?.Invoke();
            }
        }

        public override void PlayAttackSound()
        {
            body.PlaySound(_inventory.weapon.attackSound);
        }
    }
}


