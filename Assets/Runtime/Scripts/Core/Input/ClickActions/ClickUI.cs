using System.Linq;
using Map;

namespace Core.Input
{
    public class ClickUI : IClickAction
    {
        const string IGNORE_RAYCAST_TAG = "IgnoreUIRaycast";

        IScreenPositionReader _screenPositionReader;

        public ClickUI(IScreenPositionReader screenPositionReader)
        {
            _screenPositionReader = screenPositionReader;
        }

        public void ProcessClick(ITileClickData _)
        {
            //do nothing
        }

        public bool CanBeUsedOnTile(ITileClickData _)
        {
            var hits = Raycasts.UI(_screenPositionReader);

            if (hits.Any(hit => !hit.gameObject.CompareTag(IGNORE_RAYCAST_TAG)))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}