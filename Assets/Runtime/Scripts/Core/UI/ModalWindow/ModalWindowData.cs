using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Core.UI
{
    public struct ModalWindowData
	{
        public string title { get; init; }
		public string mainText { get; init; }
		public Sprite mainImage { get; init; }
		public IContextAction action { get; init; }
		public IEnumerable<ItemSlotData> resourcesData { get; init; }

        public ModalWindowData(string title, string mainText, Sprite sprite = default, IContextAction action = default)
        {
            this.title = title;
            this.mainText = mainText;
			this.mainImage = sprite;
			this.action = action;
			this.resourcesData = default;
        }

        //no image
        public ModalWindowData(string title, string mainText, IContextAction action, IEnumerable<ItemSlotData> resourcesData)
        {
            this.title = title;
            this.mainText = mainText;
			this.mainImage = default;
			this.action = action;
			this.resourcesData = resourcesData;
        }

	}
}


