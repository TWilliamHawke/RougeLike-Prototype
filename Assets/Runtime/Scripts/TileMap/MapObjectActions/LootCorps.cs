using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Objects
{
	[CreateAssetMenu(menuName ="Map/Actions/LootCorps", fileName ="LootCorps")]
    public class LootCorps : MapObjectAction
    {
        public override IMapActionLogic actionLogic => new LootCorpsLogic(this);

        public override void DoAction()
        {
            throw new System.NotImplementedException();
        }

        class LootCorpsLogic: IMapActionLogic
		{
			LootCorps _action;

            public LootCorpsLogic(LootCorps action)
            {
                _action = action;
            }
        }

    }
}

