using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DragAndDrop;
using UnityEngine.UI;

namespace Items.UI
{
    public class StorageButton : MonoBehaviour, IDropTarget<ItemSlotData>
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] Text _buttonText;

        const string DEFAULT_TEXT = "Storage";

        public bool checkImageAlpha => false;

        public void SetWelcomeText()
        {
            _buttonText.text = "Drag item to move into storage";
        }

        public void SetDefaultText()
        {
            _buttonText.text = DEFAULT_TEXT;
        }

        bool IDropTarget<ItemSlotData>.DataIsMeet(ItemSlotData data)
        {
            return data?.item != null;
        }

        void IDropTarget<ItemSlotData>.DropData(ItemSlotData data)
        {
            Debug.Log("Drop");
        }
    }
}