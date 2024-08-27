using Abilities;

namespace Items
{
    public interface IItemWithAbility
    {
        void UseAbility(AbilityController controller);
    }
}