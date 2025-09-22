using System.Collections.Generic;

namespace Core.UI
{
    public interface IContextMenu
	{
		void Fill(IEnumerable<ContextActionContainer> actions);
		void AddButtonsObserver(IObserver<IContextActionButton> observer);
		void CloseMenu();
	}
}


