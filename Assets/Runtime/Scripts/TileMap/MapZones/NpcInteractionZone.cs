using System.Collections;
using System.Collections.Generic;
using Entities.NPC;
using UnityEngine;

namespace Map.Zones
{
    public class NpcInteractionZone : MapZone, IInjectionTarget
    {
        public override IMapActionList mapActionList => _zoneLogic.actionList;
        public override TaskData currentTask => _zoneLogic.currentTask;
        public bool waitForAllDependencies => false;

        public override MapZonesObserver mapZonesObserver => _mapZonesObserver;

        IMapZoneLogic _zoneLogic;

        [SerializeField] Injector _zoneObserverInjector;
        [InjectField] MapZonesObserver _mapZonesObserver;

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


