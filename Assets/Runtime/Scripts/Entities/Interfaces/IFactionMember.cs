using Entities.Behavior;
using UnityEngine.Events;

namespace Entities
{
	public interface IFactionMember : IEntityComponent
	{
		Faction faction { get; }
        BehaviorType behavior { get; }
        event UnityAction<Faction> OnFactionChange;
        void ReplaceFaction(Faction newFaction);
	}
}


