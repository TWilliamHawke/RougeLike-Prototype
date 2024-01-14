namespace Core
{
	public abstract class RadialActionFactory<T> : IActionFactory<T>
    {
		protected abstract IRadialMenuAction CreateAction(T element);
		protected abstract bool ElementIsValid(T element);

        public bool TryCreateAction(T element, out IRadialMenuAction action)
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


