using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Objects
{
	[CreateAssetMenu(menuName ="Map/Actions/Talk", fileName ="Talk")]
    public class Talk : MapObjectAction
    {
        public override IMapActionLogic actionLogic => new TalkLogic(this);

        public override void DoAction()
        {
            throw new System.NotImplementedException();
        }


		class TalkLogic: IMapActionLogic
		{
			Talk _action;

            public TalkLogic(Talk action)
            {
                _action = action;
            }
        }
    }
}

