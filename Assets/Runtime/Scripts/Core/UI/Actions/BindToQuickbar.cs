using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BindToQuickbar<T> : RadialActionFactory<T>
    {
        protected override IRadialMenuAction CreateAction(T element)
        {
            return new BindToQuickbarAction<T>(element);
        }

        protected override bool ElementIsValid(T element)
        {
            return true;
        }

        class BindToQuickbarAction<U> : IRadialMenuAction
        {
            U _element;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.topRight;
            public string actionTitle => "Bind To Quickbar";

            public BindToQuickbarAction(U element)
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
