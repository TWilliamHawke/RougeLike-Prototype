using Entities;
using Items;

namespace Map
{
    public interface IMapActionLocation
    {
        IInteractiveStorage inventory { get; }
        void ReplaceFactionForAll(Faction replacer);
    }
}

