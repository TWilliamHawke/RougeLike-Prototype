using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace Core.UI
{
    public class BindToQuickbar<T> : ContextActionFactory<T>
    {
        PlayerAbilitiesFactory _abilitiesFactory;
        QuickBarSetupController _quickBarSetupController;

        public BindToQuickbar(PlayerAbilitiesFactory abilitiesFactory, QuickBarSetupController quickBarSetupController)
        {
            _abilitiesFactory = abilitiesFactory;
            _quickBarSetupController = quickBarSetupController;
        }

        protected override ContextActionContainer CreateAction(T element)
        {
            var container = default(IAbilityContainer);

            if (element is IAbilitySource abilitySource)
            {
                container = abilitySource.CreateAbilityContainer(_abilitiesFactory);
                _quickBarSetupController.OpenSetupScreen(container);
            }

            return new BindToQuickbarAction(container, _quickBarSetupController);
        }

        protected override bool ElementIsValid(T element)
        {
            return element is IAbilitySource;
        }

        class BindToQuickbarAction : ContextActionContainer
        {
            QuickBarSetupController _quickBarSetupController;
            IAbilityContainer _container;

            public BindToQuickbarAction(IAbilityContainer container, QuickBarSetupController quickBarSetupController)
            {
                _container = container;
                _quickBarSetupController = quickBarSetupController;
            }

            public override void DoAction()
            {
                _quickBarSetupController.OpenSetupScreen(_container);
            }
        }

    }
}
