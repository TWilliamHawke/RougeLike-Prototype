using Map;

namespace Abilities
{
    public interface IAbility : IIconData
    {
        void Use(IAbilityTarget target);
        void Select(IAbilityContainer container);
        IAbilityTarget SelectTarget(ITileClickData tile);
        string GetDescription(AbilityModifiers abilityModifiers);
        bool TileHasValidTarget(ITileClickData tile);
        bool fitForMainSlot { get; }
        public void BindAbilityUser(IAbilityUser user);
    }
}