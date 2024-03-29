using System.Collections;
using System.Collections.Generic;
using Items;
using Map.Actions;

namespace Map
{
	public interface IMapActionsController: IMapActionList
	{
		void AddAction(MapActionTemplate actionTemplate);
		void AddLootAction(MapActionTemplate actionTemplate, IEnumerable<IHaveLoot> enemies);
	}

	public interface IMapActionList
	{
	    IMapAction this[int idx] { get; }
		int count { get; }
	}

}

