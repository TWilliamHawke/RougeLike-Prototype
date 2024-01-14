using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DragAndDrop;
using UnityEngine.UI;
using Core.UI;

namespace Magic.UI
{
	public class DraggedSpell : DragableUIElement<IActionBearer>
	{
		[SerializeField] Image _spellIcon;

        public override void ApplyData(IActionBearer data)
        {
            _spellIcon.sprite = data.icon;
        }


	}
}