using Map.Actions;

namespace Map
{
    public interface INpcActionCreator
    {
        IMapAction CreateActionLogic(MapActionTemplate template, INpcActionTarget actionTarget);
    }
}

