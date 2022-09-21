using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Map.Objects
{
	[CreateAssetMenu(menuName ="Map/Actions/Loot", fileName ="Loot")]
    public class Loot : MapObjectAction
    {
		[SerializeField] LootTable _lootTable;

        public override IMapActionLogic actionLogic => new LootLogic(this);

        public override void DoAction()
        {
            
        }

		class LootLogic: IMapActionLogic
		{
			Loot _action;

            public LootLogic(Loot action)
            {
                _action = action;
            }
        }
    }
}

