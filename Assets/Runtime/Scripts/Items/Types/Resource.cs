using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	[CreateAssetMenu(fileName = "Resource", menuName ="Musc/Resourse")]
	public class Resource : Item
	{
		[SerializeField] ResourceType _resourceType;


		public ResourceType type => _resourceType;
	}

}