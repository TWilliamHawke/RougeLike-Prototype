using System.Collections.Generic;

namespace Core.Input
{
    public interface IClickActionList : IEnumerable<IClickAction>
    {
        void CleanUp();
    }
}