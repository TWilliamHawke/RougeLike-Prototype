using System.Collections.Generic;
using Items;
using Map.Objects;

namespace Map
{
    public interface IMapActionsFactory
    {
        IMapAction CreateActionLogic(MapActionTemplate template, int numOfUsage);
        IMapAction CreateLootAction(MapActionTemplate template, ILootStorage loot);
        IMapAction CreateNPCAction(MapActionTemplate template);
    }
}


