using System.Collections;
using System.Collections.Generic;
using Items;
using Items.UI;
using UnityEngine;

namespace Map.Objects
{
	[CreateAssetMenu(menuName ="Map/Actions/LootBodies", fileName ="LootBodies")]
    public class LootBodies: ScriptableObject, IActionData
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;

        public LootBodiesLogic actionLogic => new LootBodiesLogic(this);

        public string displayName => _displayName;
        public Sprite icon => _icon;

        public class LootBodiesLogic: IMapActionLogic
		{
			LootBodies _action;
            public bool isEnable { get; set; }
            LootPanel _lootPanel;
            ItemSection<Item> _loot;
            public IActionData template => _action;

            public LootBodiesLogic(LootBodies action)
            {
                _action = action;
            }

            public void AddActionDependencies(IActionDependenciesProvider provider)
            {
                Debug.Log("Add");
                _lootPanel = provider.lootPanel;
            }

            public void CreateLoot(IEnumerable<IHaveLoot> enemies)
            {
                _loot = new ItemSection<Item>(-1);
                foreach(var enemy in enemies)
                {
                    enemy.lootTable.GetLoot(ref _loot);
                }
            }

            public void DoAction()
            {
                _lootPanel.Open(_loot);
            }
        }

    }
}

