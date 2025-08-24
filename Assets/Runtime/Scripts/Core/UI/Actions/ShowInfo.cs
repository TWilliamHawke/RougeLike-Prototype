using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class ShowInfo<T> : ContextActionFactory<T>
    {
        protected override ContextActionContainer CreateAction(T element)
        {
            return new ShowInfoAction<T>(element);
        }

        protected override bool ElementIsValid(T element)
        {
            return true;
        }

        class ShowInfoAction<U> : ContextActionContainer
        {
            U _element;

            public ShowInfoAction(U element)
            {
                _element = element;
            }

            public override void DoAction()
            {
                Debug.Log(actionTitle);
            }
        }

    }
}
