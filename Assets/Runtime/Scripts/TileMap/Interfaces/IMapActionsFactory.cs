using System.Collections.Generic;
using Items;
using Map.Actions;

namespace Map
{
    public interface IMapActionsFactory
    {
        IMapAction CreateActionLogic(MapActionTemplate template, int numOfUsage);
        IMapAction CreateLootAction(MapActionTemplate template, ILootStorage loot);
        IMapAction CreateNPCAction(MapActionTemplate template, INpcActionTarget actionTarget, int numOfUsage = -1);
    }
}


