using Map.Actions;

namespace Map
{
    public interface IMapActionCreator
    {
        IMapAction CreateActionLogic(MapActionTemplate template, int numOfUsage);
    }
}

