using Entities;
using Items;

namespace Map
{
    public interface IMapActionLocation
    {
        IContainersList inventory { get; }
        void ReplaceFactionForAll(Faction replacer);
    }
}

