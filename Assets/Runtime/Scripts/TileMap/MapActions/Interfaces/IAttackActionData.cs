using Entities;

namespace Map
{
    public interface IAttackActionData: IIconData
    {
        Faction enemyFaction { get; }
    }
}

