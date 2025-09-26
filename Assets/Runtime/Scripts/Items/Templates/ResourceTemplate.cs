using System.Collections;
using System.Collections.Generic;
using UI.Tooltips;
using UnityEngine;

namespace Items
{
	[CreateAssetMenu(fileName = "Resource", menuName ="Musc/Resourse")]
	public class ResourceTemplate : ItemTemplate, IItem
	{
		[SerializeField] ResourceType _resourceType;
		[SerializeField] int _startCount;

		[SerializeField] string _description;


		public ResourceType type => _resourceType;
		public int startCount => _startCount;

        public override IItem CreateItem(int rarity = 0)
        {
			return this;
        }

        public override string GetDescription()
        {
            return _description;
        }

        public ItemTooltipData GetTooltipData()
        {
            throw new System.NotImplementedException();
        }
    }

}