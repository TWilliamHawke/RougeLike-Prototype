using System.Collections.Generic;

namespace Core.Input
{
    public interface IClickActionList
    {
        void CleanUp();
        IEnumerable<IClickAction> GetActions();
    }
}