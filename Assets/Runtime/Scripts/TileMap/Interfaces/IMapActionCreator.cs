using Map.Objects;

namespace Map
{
    public interface IMapActionCreator
    {
        IMapAction CreateActionLogic(MapActionTemplate template);
    }
}

