using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace Core
{
    public class BindToQuickbar<T> : RadialActionFactory<T>
    {
        PlayerAbilitiesFactory _abilitiesFactory;
        QuickBarSetupController _quickBarSetupController;

        public BindToQuickbar(PlayerAbilitiesFactory abilitiesFactory, QuickBarSetupController quickBarSetupController)
        {
            _abilitiesFactory = abilitiesFactory;
            _quickBarSetupController = quickBarSetupController;
        }

        protected override IRadialMenuAction CreateAction(T element)
        {
            return new BindToQuickbarAction<T>(element, _abilitiesFactory, _quickBarSetupController);
        }

        protected override bool ElementIsValid(T element)
        {
            return element is IAbilitySource;
        }

        class BindToQuickbarAction<U> : IRadialMenuAction
        {
            U _element;
        PlayerAbilitiesFactory _abilitiesFactory;
        QuickBarSetupController _quickBarSetupController;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.topRight;
            public string actionTitle => "Bind To Quickbar";

            public BindToQuickbarAction(U element, PlayerAbilitiesFactory abilitiesFactory, QuickBarSetupController quickBarSetupController)
            {
                _element = element;
                _abilitiesFactory = abilitiesFactory;
                _quickBarSetupController = quickBarSetupController;
            }

            public void DoAction()
            {
                if(_element is IAbilitySource abilitySource)
                {
                    var container = abilitySource.CreateAbilityContainer(_abilitiesFactory);
                    _quickBarSetupController.OpenSetupScreen(container);
                }
            }
        }

    }
}
