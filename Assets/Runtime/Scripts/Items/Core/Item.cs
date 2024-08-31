using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Tooltips;

namespace Items
{
    public abstract class Item : ScriptableObject, IIconData
	{
		[UseFileName]
	    [SerializeField] string _displayName;
		[SpritePreview]
		[SerializeField] Sprite _icon;
        [SerializeField] ItemType _itemType;
		[SerializeField] int _maxStackSize = 1;
		[SerializeField] int _value;
		[SerializeField] ItemSoundKit _soundKit;

	    public virtual string displayName => _displayName;
		public Sprite icon => _icon;
        public ItemType itemType => _itemType;
		public int maxStackSize => _maxStackSize;
		public virtual int value => _value;
		public AudioClip useSound => _soundKit.useSound;
		public AudioClip dragSound => _soundKit.dragSound;

		public abstract string GetDescription();
		public abstract string GetItemType();

		public ItemTooltipData GetTooltipData()
		{
			var tooltipData = new ItemTooltipData();
			tooltipData.icon = _icon;
			tooltipData.title = _displayName;
			tooltipData.itemType = GetItemType();
			tooltipData.description = GetDescription();

			return tooltipData;
		}

	}
}