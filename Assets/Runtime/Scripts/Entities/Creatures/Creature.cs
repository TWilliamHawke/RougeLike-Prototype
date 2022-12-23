using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Entities.PlayerScripts;
using Items;
using UnityEngine;

namespace Entities
{
    public class Creature : Entity
    {
        [SerializeField] CreatureTemplate _template;

        public override Dictionary<DamageType, int> resists => _template.resists.set;

        public override AudioClip[] deathSounds => _template.sounds.deathSounds;

        public override LootTable lootTable => _template.lootTable;
        public override IDamageSource damageSource => _template;
        protected override ITemplateWithBaseStats template => _template;

        public void BindTemplate(CreatureTemplate template)
        {
            _template = template;
            InitComponents();
            ApplyStartStats(template);
        }

        public override void Interact(Player player)
        {
            player.Attack(this);
        }

        public override void PlayAttackSound()
        {
            body.PlaySound(_template.attackSounds.GetRandom());
        }

    }
}


