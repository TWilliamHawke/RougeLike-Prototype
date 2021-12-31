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
        [SerializeField] DragController _dragController;
        [SerializeField] Text _buttonText;

        const string DEFAULT_TEXT = "Storage";

        public void Init()
        {
			_dragController.OnBeginDrag += SetWelcomeText;
			_dragController.OnEndDrag += SetDefaultText;
        }

        void OnDestroy()
        {
			_dragController.OnBeginDrag -= SetWelcomeText;
			_dragController.OnEndDrag -= SetDefaultText;
        }

        public bool DataIsMeet(ItemSlotData data)
        {
            return data?.item != null;
        }

        public void DropData(ItemSlotData data)
        {
            Debug.Log("Drop");
        }

		void SetWelcomeText(object data)
		{
			if(!DataIsMeet(data as ItemSlotData)) return;

			_buttonText.text = "Drag item to move into storage";
		}

		void SetDefaultText()
		{
			_buttonText.text = DEFAULT_TEXT;
		}
    }
}