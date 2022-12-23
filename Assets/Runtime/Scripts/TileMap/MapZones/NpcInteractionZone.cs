using System.Collections;
using System.Collections.Generic;
using Entities.NPCScripts;
using UnityEngine;

namespace Map.Zones
{
    public class NpcInteractionZone : MapZone, IInjectionTarget
    {
        public override IMapActionList mapActionList => _zoneLogic.actionList;
        public override TaskData currentTask => _zoneLogic.currentTask;
        public bool waitForAllDependencies => false;

        IMapZoneLogic _zoneLogic;

        [SerializeField] Injector _zoneObserverInjector;

        public void Init(IMapZoneLogic zoneLogic)
        {
            gameObject.SetActive(true);
            base.BindTemplate(zoneLogic.template);
            _zoneLogic = zoneLogic;
            _zoneObserverInjector.AddInjectionTarget(this);
        }

        public void FinalizeInjection()
        {
            AddToObserver();
        }
    }
}


