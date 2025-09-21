using Map;

namespace Abilities
{
    public interface IAbility : IIconData
    {
        void Use(IAbilityUser user, IAbilityTarget target);
        void Select(IAbilityUser user, IAbilityContainer container);
        IAbilityTarget SelectTarget(ITileClickData tile);
        string GetDescription(AbilityModifiers abilityModifiers);
        bool TileHasValidTarget(IAbilityUser user, ITileClickData tile);
        bool fitForMainSlot { get; }
    }
}