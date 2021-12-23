using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.UI;

namespace Magic.UI
{
    public class ActiveStringSlot : MonoBehaviour
    {
        [SerializeField] Image _icon;
        [SerializeField] Sprite _defaultIcon;

        public void SetData(SpellString data)
        {
            _icon.sprite = data?.icon ?? _defaultIcon;
        }

    }
}