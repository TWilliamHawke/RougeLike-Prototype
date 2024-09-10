using System.Collections.Generic;
using Items;
using Map.Actions;

namespace Map
{
    public interface IMapActionsFactory
    {
        IMapAction CreateAction(MapActionTemplate template, IMapActionLocation store);
        IMapAction CreateLootAction(MapActionTemplate template, IContainersList loot);
    }
}


