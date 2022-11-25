using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DragAndDrop;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Items.UI
{
    public class InventoryScreen : MonoBehaviour, IUIScreen
    {
        [SerializeField] DragController _dragController;
        [Header("UI Elements")]
        [SerializeField] ContextMenu _contextMenu;
        [SerializeField] InventorySection _potionsBag;
        [SerializeField] InventorySection _scrollsBag;
        [SerializeField] InventorySection _main;
        [SerializeField] StorageButton _storageButton;
        [SerializeField] Image _background;

        public InventorySection potionsBag => _potionsBag;
        public InventorySection scrollsBag => _scrollsBag;
        public InventorySection mainSection => _main;

        public event UnityAction OnScreenOpen;

        public void Open()
        {
            gameObject.SetActive(true);
            OnScreenOpen?.Invoke();
        }

        public void Init()
        {
            _storageButton.Init();

            _dragController.OnBeginDrag += HideBackGround;
            _dragController.OnEndDrag += ShowBackGround;
        }

        void OnEnable()
        {
            _contextMenu.TryInit();
        }

        void OnDestroy()
        {
            _dragController.OnBeginDrag -= HideBackGround;
            _dragController.OnEndDrag -= ShowBackGround;
        }

        void ShowBackGround()
        {
            _background.gameObject.SetActive(true);
        }

        void HideBackGround(object _)
        {
            _background.gameObject.SetActive(false);
        }

    }
}