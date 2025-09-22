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
            IAbilityUser _abilityUser;

            public UseAbilityAction(AbilityController abilityController, IAbilityContainer abilityContainer)
            {
                _abilityUser = abilityController;
                _abilityContainer = abilityContainer;
            }

            public override void DoAction()
            {
                if (_abilityContainer is null) return;
                _abilityContainer.SelectBy(_abilityUser);
            }

        }

    }
}
