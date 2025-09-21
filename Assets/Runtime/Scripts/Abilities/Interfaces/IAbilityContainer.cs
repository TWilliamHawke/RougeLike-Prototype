using Map;

namespace Abilities
{
    public interface IAbilityContainer : IAbilityContainerData
    {
        void UseAbility(ITileClickData tile);
        void UseAbility(IAbilityTarget target);
        void SelectBy(IAbilityUser user);
        bool canBeUsed { get; }
        bool TileHasValidTarget(ITileClickData tile);
        bool fitForMainSlot { get; }
    }
}