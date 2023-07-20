using UnityEngine.Events;

namespace Entities
{
	public interface IFactionMember
	{
		Faction faction { get; }
        event UnityAction<Faction> OnFactionChange;
        void ReplaceFaction(Faction newFaction);
	}
}


