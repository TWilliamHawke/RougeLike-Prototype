using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	[CreateAssetMenu(fileName = "Resource", menuName ="Musc/Resourse")]
	public class ResourceTemplate : ItemTemplate
	{
		[SerializeField] ResourceType _resourceType;
		[SerializeField] int _startCount;

		[SerializeField] string _description;


		public ResourceType type => _resourceType;
		public int startCount => _startCount;

        public override string GetDescription()
        {
            return _description;
        }
    }

}