using Abilities;

namespace Core.UI
{
    public abstract class UseAbility<T> : ContextActionFactory<T>
    {
        IAbilitiesFactory _abilitiesFactory;
        AbilityController _abilityController;

        public UseAbility(IAbilitiesFactory abilitiesFactory, AbilityController abilityController)
        {
            _abilitiesFactory = abilitiesFactory;
            _abilityController = abilityController;
        }

        protected ContextActionContainer CreateAction(IAbilitySource abilitySource)
        {
            var container = abilitySource.CreateAbilityContainer(_abilitiesFactory);
            return new UseAbilityAction(_abilityController, container);
        }

        class UseAbilityAction : ContextActionContainer
        {
            IAbilityContainer _abilityContainer;
            AbilityController _abilityController;

            public UseAbilityAction(AbilityController abilityController, IAbilityContainer abilityContainer)
            {
                _abilityController = abilityController;
                _abilityContainer = abilityContainer;
            }

            public override void DoAction()
            {
                if (_abilityContainer is null) return;
                _abilityController.SelectAbility(_abilityContainer);
            }

        }

    }
}
