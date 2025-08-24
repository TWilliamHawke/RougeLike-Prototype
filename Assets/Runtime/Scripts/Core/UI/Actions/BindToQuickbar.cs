using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace Core.UI
{
    public abstract class BindToQuickbar<T> : ContextActionFactory<T>
    {
        PlayerAbilitiesFactory _abilitiesFactory;
        QuickBarSetupController _quickBarSetupController;

        public BindToQuickbar(PlayerAbilitiesFactory abilitiesFactory, QuickBarSetupController quickBarSetupController)
        {
            _abilitiesFactory = abilitiesFactory;
            _quickBarSetupController = quickBarSetupController;
        }

        protected ContextActionContainer CreateAction(IAbilitySource abilitySource)
        {
            var container = abilitySource.CreateAbilityContainer(_abilitiesFactory);
            return new BindToQuickbarAction(container, _quickBarSetupController);
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
