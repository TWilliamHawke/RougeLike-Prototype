using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    [CreateAssetMenu(menuName = "Map/Actions/Attack", fileName = "Attack")]
    public class Attack : MapObjectAction
    {
        public override IMapActionLogic actionLogic => new AttackLogic(this);

        class AttackLogic : IMapActionLogic
        {
            Attack _action;
            public event UnityAction<IMapActionLogic> OnCompletion;
            public bool isEnable { get; set; } = true;

            public IActionData template => _action;

            public AttackLogic(Attack action)
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

