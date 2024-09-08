using System.Collections.Generic;

namespace Items
{
    public interface IContainersList
    {
        ItemContainer ContainerAt(int idx);
        int count { get; }
        IEnumerable<ItemContainer> GetAllContainers();
    }
}


