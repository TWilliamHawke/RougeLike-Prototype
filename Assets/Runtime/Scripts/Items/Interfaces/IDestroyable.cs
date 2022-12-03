using System.Collections.Generic;

namespace Items
{
	public interface IDestroyable
	{
	    void AddItemComponentsTo(Inventory inventory);
		void AddItemComponentsTo(ref List<ItemSlotData> items);
	}
}