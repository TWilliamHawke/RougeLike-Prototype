using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
	[CreateAssetMenu(fileName = "Resource", menuName ="Musc/Resourse")]
	public class Resource : ScriptableObject
	{
		[UseFileName]
	    [SerializeField] string _displayName;
		[SpritePreview]
		[SerializeField] Sprite _icon;
		[SerializeField] int _maxCount;
		[SerializeField] ResourceType _resourceType;


		public ResourceType type => _resourceType;
		public int maxCount => _maxCount;
	}

}