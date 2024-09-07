using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Entities.PlayerScripts;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Creature : Entity
    {
        [SerializeField] CreatureTemplate _template;

        public override Dictionary<DamageType, int> resists => _template.resists.set;

        public override AudioClip[] deathSounds => _template.sounds.deathSounds;

        public override LootTable lootTable => _template.lootTable;
        public override IDamageSource damageSource => _template;
        public override ITemplateWithBaseStats template => _template;

        public override event UnityAction<ITemplateWithBaseStats> OnTemplateApplied;

        public void BindTemplate(CreatureTemplate template)
        {
            _template = template;
            InitComponents();
            ApplyStartStats(template);
            OnTemplateApplied?.Invoke(template);
        }

        public override void Interact(Player player)
        {
            player.Attack(this);
        }

        public override void PlayAttackSound()
        {
            body.PlaySound(_template.attackSounds.GetRandom());
        }

        public override void AddLoot(ILootContainer container)
        {
            container.AddItems(_template.lootTable);
        }

        public override void RemoveLoot(ILootContainer container)
        {

        }
    }
}


