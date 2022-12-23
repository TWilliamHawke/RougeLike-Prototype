namespace Map.Zones
{
    public interface IMapZoneLogic
    {
        IMapActionList actionList { get; }
        TaskData currentTask { get; }
        IMapZoneTemplate template { get; }
    }
}

