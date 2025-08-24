using Abilities;
using Core;

namespace Items.Actions
{
    public class UseAbility : ContextActionFactory<ItemSlotData>
    {
        IAbilitiesFactory _abilitiesFactory;
        AbilityController _abilityController;

        public UseAbility(IAbilitiesFactory abilitiesFactory, AbilityController abilityController)
        {
            _abilitiesFactory = abilitiesFactory;
            _abilityController = abilityController;
        }

        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            return new UseAbilityAction(itemSlot, _abilitiesFactory, _abilityController);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return itemSlot?.item is IItemWithAbility;
        }

        class UseAbilityAction : ContextActionContainer
        {
            IAbilityContainer _abilityContainer;
            IAbilityTarget _target;

            public UseAbilityAction(ItemSlotData itemSlot,
                IAbilitiesFactory abilitiesFactory, AbilityController abilityController)
            {
                var item = itemSlot.item as IAbilitySource;
                if (item is null) return;
                _target = abilityController.GetComponent<IAbilityTarget>();
                _abilityContainer = item.CreateAbilityContainer(abilitiesFactory);
            }

            public override void DoAction()
            {
                if (_abilityContainer is null) return;
                _abilityContainer.UseAbility(_target);
            }

        }
    }
}