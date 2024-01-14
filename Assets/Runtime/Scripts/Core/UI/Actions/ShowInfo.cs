using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ShowInfo<T> : RadialActionFactory<T>
    {
        protected override IRadialMenuAction CreateAction(T element)
        {
            return new ShowInfoAction<T>(element);
        }

        protected override bool ElementIsValid(T element)
        {
            return true;
        }

        class ShowInfoAction<U> : IRadialMenuAction
        {
            U _element;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.topLeft;
            public string actionTitle => "ShowInfo";

            public ShowInfoAction(U element)
            {
                _element = element;
            }

            public void DoAction()
            {
                Debug.Log(actionTitle);
            }
        }

    }
}
