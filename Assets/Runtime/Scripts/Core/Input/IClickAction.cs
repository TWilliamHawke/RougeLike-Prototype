namespace Core.Input
{
	public interface IClickAction
	{
	    bool Condition();
		void ProcessClick();
	}
}