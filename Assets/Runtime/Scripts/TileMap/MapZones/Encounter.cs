using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using Entities.NPC;
using Rng = System.Random;
using Items;


namespace Map.Zones
{
    public class Encounter : MapZone
    {
        EncounterTemplate _template;

        public void BindTemplate(EncounterTemplate template, Rng rng)
        {
            _template = template;
            base.BindTemplate(template, rng);
        }

        protected override void FillActionsList(IMapActionsController mapActionsController)
        {
            foreach(var actionTemplate in _template.possibleActions)
            {
                mapActionsController.AddAction(actionTemplate);
            }
        }
    }
}


