using Map;

namespace Core.Input
{
	public interface IClickAction
	{
	    bool CanBeUsedOnTile(ITileClickData tile);
        void ProcessClick(ITileClickData tile);
	}
}