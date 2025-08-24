using System.Collections.Generic;

namespace Core.UI
{
    public interface IContextActionSource
    {
        IEnumerable<ContextActionTemplate> GetActions();
    }
}
