using Map.Actions;

namespace Map
{
    public interface IMapActionCreator
    {
        IMapAction CreateActionLogic(MapActionTemplate template, IMapActionLocation store);
    }
}

