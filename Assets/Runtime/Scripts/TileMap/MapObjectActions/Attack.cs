using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Objects
{
	[CreateAssetMenu(menuName ="Map/Actions/Attack", fileName ="Attack")]
    public class Attack : MapObjectAction
    {
        public override IMapActionLogic actionLogic => new AttackLogic(this);

        public override void DoAction()
        {
            throw new System.NotImplementedException();
        }

		class AttackLogic: IMapActionLogic
		{
			Attack _action;

            public AttackLogic(Attack action)
            {
                _action = action;
            }
        }
    }
}

