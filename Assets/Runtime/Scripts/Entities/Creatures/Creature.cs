using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Entities.PlayerScripts;
using Items;
using Map;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Creature : Entity
    {
        [SerializeField] CreatureTemplate _template;


        public override AudioClip[] deathSounds => _template.sounds.deathSounds;

        public override ITemplateWithBaseStats template => _template;

        public override event UnityAction<ITemplateWithBaseStats> OnTemplateApplied;

        public void BindTemplate(CreatureTemplate template)
        {
            _template = template;
            ApplyStartStats(template);
            OnTemplateApplied?.Invoke(template);
        }

        public override void Interact(Player player)
        {
            player.UseMainAbility(this);
        }

        public override void AddLootTo(IItemStorage storage)
        {
            storage.AddItemsFrom(_template.lootTable);
        }

        public override void RemoveLootFrom(IItemStorage storage)
        {

        }

        public override void InitInteractiveZone(IMapZone mapZoneLogic)
        {

        }

    }
}


