
namespace Core
{
    public interface IActionFactory<T>
    {
        bool TryCreateAction(T data, out ContextActionContainer action);
	}
}


