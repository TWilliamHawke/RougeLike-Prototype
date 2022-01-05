using Effects;

namespace Items
{
    public interface IItemWithAbility
    {
        void UseAbility(AbilityController controller);
    }
}