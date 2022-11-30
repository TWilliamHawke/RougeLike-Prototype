using System.Collections.Generic;

namespace Core.UI
{
    public interface IContextMenu
	{
		void Fill(IEnumerable<IContextAction> actions);
	}
}


