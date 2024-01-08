using Core;

namespace Items
{
	public interface IActionFactory<T>
	{
		bool TryCreateAction(T data, out IRadialMenuAction action);
	}
}


