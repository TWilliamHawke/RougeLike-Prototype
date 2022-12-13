namespace Map
{
    public interface IMapObjectTemplate : IIconData
    {
        int width { get; }
        int height { get; }
        int centerZoneWidth { get; }
        int centerZoneHeight { get; }
        bool centerZoneIsWalkable { get; }
    }
}

