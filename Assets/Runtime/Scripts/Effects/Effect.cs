using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public class Effect : ScriptableObject
	{
	    [SerializeField] string _displayName;
		[Multiline(4)]
		[SerializeField] string _description;
		[SerializeField] Sprite _icon;
	}
}