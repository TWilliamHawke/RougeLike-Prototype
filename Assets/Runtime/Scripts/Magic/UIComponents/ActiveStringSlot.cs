using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.UI;
using UI.DragAndDrop;
using UnityEngine.EventSystems;
using MouseButton = UnityEngine.EventSystems.PointerEventData.InputButton;
using UnityEngine.Events;

namespace Magic.UI
{
    public class ActiveStringSlot : MonoBehaviour, IDropTarget<ItemSlotData>, IPointerClickHandler
    {
        [SerializeField] Sprite _defaultIcon;
        [Header("UI Elements")]
        [SerializeField] Image _icon;
        [SerializeField] Image _dragMask;
        [SerializeField] Image _pointer;

        int _slotIndex = -1;

        public bool checkImageAlpha => false;

        public event UnityAction<int> OnClick;
        public event UnityAction<ItemSlotData> OnStringDrop;

        void OnEnable()
        {
            _pointer.Hide();
            _dragMask.Hide();
        }

        public void Init(int slotIndex)
        {
            _slotIndex = slotIndex;
        }

        public void SetSelection(bool state)
        {
            if (state)
            {
                _pointer.Show();
            }
            else
            {
                _pointer.Hide();
            }
        }

        public bool DataIsMeet(ItemSlotData data)
        {
            return data != null && data.item is SpellString;
        }

        public void SetIcon(IIconData iconData)
        {
            if (iconData == null)
            {
                _icon.Hide();
                return;
            }
            _icon.Show();
            _icon.sprite = iconData.icon;
        }

        public void DropData(ItemSlotData slotData)
        {
            OnStringDrop?.Invoke(slotData);
        }

        //used as unityevent
        public void ShowDragMask()
        {
            _dragMask.Show();
        }

        //used as unityEvent
        public void HideDragMask()
        {
            _dragMask.Hide();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(_slotIndex);
        }
    }
}