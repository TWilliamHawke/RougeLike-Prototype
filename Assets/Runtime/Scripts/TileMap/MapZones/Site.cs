using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.Events;
using Rng = System.Random;
using Map.Actions;
using Items;

namespace Map.Zones
{
    public class Site : MapZone
    {
        [SerializeField] MapActionTemplate _lootBodiesAction;
        [SerializeField] InteractionZone _interactionZone;

        SiteTemplate _template;

        //should be called in Awake
        public void BindTemplate(SiteTemplate template, Rng rng)
        {
            _template = template;
            _interactionZone.Init(this);
            _interactionZone.Resize(template.size);

            base.BindTemplate(template, rng);
        }

        protected override void FillActionsList(IMapActionsController mapActionsController)
        {
            mapActionsController.AddLootAction(_lootBodiesAction, _deadEntitiesStorage);
            _template.possibleActions.ForEach(action => mapActionsController.AddAction(action));
        }
    }
}

