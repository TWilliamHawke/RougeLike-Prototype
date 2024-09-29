namespace Items
{
    public interface IInteractiveStorage : IContainersList
    {
        bool isStealingTarget { get; set; }
    }
}


