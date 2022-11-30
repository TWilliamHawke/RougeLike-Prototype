using System.Collections;
using System.Collections.Generic;
using Items.Actions;
using UnityEngine;
using Core.UI;
using UnityEngine.EventSystems;
using System.Linq;

namespace Items.UI
{
    [RequireComponent(typeof(ItemSlot))]
    public class ItemContextMenuProvider : MonoBehaviour, IInjectionTarget, IBeginDragHandler
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] Injector _contextMenuInjector;

        [InjectField] IContextMenu _contextMenu;

        List<IItemAction> _itemActions = new();

        public bool waitForAllDependencies => false;


        private void Awake()
        {
            var itemSlot = GetComponent<IItemSlot>();
            _contextMenuInjector.AddInjectionTarget(this);

            _itemActions.Add(new Use(itemSlot));
            _itemActions.Add(new Buy(itemSlot));
            _itemActions.Add(new Cell(itemSlot));
            _itemActions.Add(new Equip(itemSlot));
            _itemActions.Add(new MoveToStorage(itemSlot));
            _itemActions.Add(new Destroy(itemSlot, _inventory));
            _itemActions.Add(new Drop(itemSlot));

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //HACK it should be SlotIsValid() <= zero args
            _contextMenu.Fill(_itemActions.Where(action => action.SlotIsValid(GetComponent<IItemSlot>())));
        }
        
        void IInjectionTarget.FinalizeInjection()
        {
        }

    }
}


