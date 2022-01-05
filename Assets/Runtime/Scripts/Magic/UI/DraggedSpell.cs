using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DragAndDrop;
using UnityEngine.UI;

namespace Magic.UI
{
	public class DraggedSpell : DragableUIElement<KnownSpellData>
	{
		[SerializeField] Image _spellIcon;

        protected override void ApplyData(KnownSpellData data)
        {
            _spellIcon.sprite = data.icon;
        }


	}
}