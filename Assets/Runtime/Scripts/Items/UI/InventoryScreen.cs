using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DragAndDrop;
using UnityEngine.UI;

namespace Items.UI
{
    public class InventoryScreen : MonoBehaviour, IUIScreen
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] DragController _dragController;
        [Header("UI Elements")]
        [SerializeField] InventorySection _potionsBag;
        [SerializeField] InventorySection _scrollsBag;
        [SerializeField] InventorySection _main;
        [SerializeField] StorageButton _storageButton;
        [SerializeField] Image _background;

        public void Init()
        {
            _potionsBag.SetSectionData(_inventory.potionsBag);
            _scrollsBag.SetSectionData(_inventory.scrollsBag);
            _main.SetSectionData(_inventory.main);
            _storageButton.Init();

            _dragController.OnBeginDrag += HideBackGround;
            _dragController.OnEndDrag += ShowBackGround;
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