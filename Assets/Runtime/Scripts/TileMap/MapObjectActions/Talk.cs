using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    [CreateAssetMenu(menuName = "Map/Actions/Talk", fileName = "Talk")]
    public class Talk : MapObjectAction
    {
        public override IMapActionLogic actionLogic => new TalkLogic(this);

        class TalkLogic : IMapActionLogic
        {
            Talk _action;

            public event UnityAction<IMapActionLogic> OnCompletion;

            public bool isEnable { get; set; } = true;
            public IActionData template => _action;

            public TalkLogic(Talk action)
            {
                _action = action;
            }

            public void DoAction()
            {
                OnCompletion?.Invoke(this);
                throw new System.NotImplementedException();
            }

            public void AddActionDependencies(IActionDependenciesProvider provider)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}

