namespace Core
{
	public abstract class ContextActionFactory<T> : IActionFactory<T>
    {
		protected abstract ContextActionContainer CreateAction(T element);
		protected abstract bool ElementIsValid(T element);

        public bool TryCreateAction(T element, out ContextActionContainer action)
        {
            action = default;
            if (ElementIsValid(element))
            {
                action = CreateAction(element);
                return true;
            }
            return false;
        }
    }
}


