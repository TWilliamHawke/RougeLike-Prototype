using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    class Talk : IMapActionLogic
    {
        IIconData _action;

        public event UnityAction<IMapActionLogic> OnCompletion;

        public bool isEnable { get; set; } = true;
        public IIconData template => _action;

        public bool waitForAllDependencies => false;

        public Talk(IIconData action)
        {
            _action = action;
        }

        public void DoAction()
        {
            OnCompletion?.Invoke(this);
        }

        public void FinalizeInjection()
        {

        }
    }
}

