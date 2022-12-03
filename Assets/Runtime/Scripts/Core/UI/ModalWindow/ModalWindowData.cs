using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class ModalWindowData : MonoBehaviour
	{
		public string title { get; set; }
		public string mainText { get; set; }
		public Sprite mainImage { get; set; }
		public IContextAction action { get; set; }
		public List<Sprite> resourceIcons { get; init; } = new();
		public List<Sprite> resourceDesription { get; init; } = new();
	}
}


