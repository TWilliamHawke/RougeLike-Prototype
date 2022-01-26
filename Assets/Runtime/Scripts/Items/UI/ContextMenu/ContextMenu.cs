using System;
using System.Collections;
using System.Collections.Generic;
using Items.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Items.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class ContextMenu : MonoBehaviour
    {
        [SerializeField] ItemActionButton _actionButtonPrefab;
        [SerializeField] LayoutGroup _actionsLayout;
        [SerializeField] Inventory _inventory;

        bool _isInit = false;

        RectTransform _rectTransform;
        RectTransform _rectTransformLayout;

        List<ItemActionButton> _actionButtons = new List<ItemActionButton>();

        public void TryInit()
        {
            if (_isInit) return;
            ItemSlot.OnRightClick += ShowContextMenu;
            _rectTransform = GetComponent<RectTransform>();
            _rectTransformLayout = _actionsLayout.GetComponent<RectTransform>();
            ItemActionButton.OnClick += Close;

            CreateMenuButtons();

            _isInit = true;
        }

        void OnDestroy()
        {
            ItemActionButton.OnClick -= Close;

        }

        void CreateMenuButtons()
        {
            List<IItemAction> itemActions = new List<IItemAction>();
            foreach (Transform children in _actionsLayout.transform)
            {
                Destroy(children.gameObject);
            }


            itemActions.Add(new Use());
            itemActions.Add(new Buy());
            itemActions.Add(new Cell());
            itemActions.Add(new Equip());
            itemActions.Add(new MoveToStorage());
            itemActions.Add(new Destroy(_inventory));
            itemActions.Add(new Drop());

            foreach (var action in itemActions)
            {
                var actionButton = Instantiate(_actionButtonPrefab, transform);
                actionButton.BindAction(action);
                actionButton.transform.SetParent(_actionsLayout.transform);
                _actionButtons.Add(actionButton);
            }



        }

        void ShowContextMenu(IItemSlot itemSlot)
        {
            if (itemSlot.itemSlotData?.item == null) return;

            foreach (var actionButton in _actionButtons)
            {
                actionButton.CheckItemSlot(itemSlot);
            }


            //LayoutRebuilder.ForceRebuildLayoutImmediate(_actionsLayout.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
            SetMenuPosition();

            gameObject.SetActive(true);

        }

        void SetMenuPosition()
        {
            Vector2 menuSize = _rectTransformLayout.sizeDelta;
            var mousePos = Mouse.current.position.ReadValue().AddZ(0);

            float offsetX = mousePos.x + menuSize.x > Screen.width ? -menuSize.x / 2 : menuSize.x / 2;
            float offsetY = mousePos.y + menuSize.y > Screen.height ? -menuSize.y / 2 : menuSize.y / 2;

            _rectTransform.position = mousePos + new Vector3(offsetX, offsetY, 0);
        }

        void Close()
        {
            gameObject.SetActive(false);
        }
    }
}