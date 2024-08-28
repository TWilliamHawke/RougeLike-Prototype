using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DragAndDrop;
using Items;
using Effects;
using Entities.PlayerScripts;
using Abilities;

namespace Abilities
{
    public class QuickBar : MonoBehaviour
	{
	    [SerializeField] QuickBarSlot[] _quickBarSlots;
        [SerializeField] QuickBarSlot _mainSlot;
        [SerializeField] QuickBarDataStorage _quickBarDataStorage;

        [InjectField] Player _player;

        AbilityController _abilityController;

        void Awake()
        {
            SetUpSlotNumbers();
            _quickBarDataStorage.OnQuickBarChange += UpdateSlots;
        }

        void OnDestroy()
        {
            _quickBarDataStorage.OnQuickBarChange -= UpdateSlots;
        }

        //Used in Unity Editor
        public void FindAbilityController()
        {
            _abilityController = _player.GetComponent<AbilityController>();
            UpdateSlots();
        }

        private void SetUpSlotNumbers()
        {
            for (int i = 0; i < _quickBarSlots.Length; i++)
            {
                _quickBarSlots[i].SetSlotNumber(i);
            }
        }

        private void UpdateSlots()
        {
            if (_quickBarDataStorage.mainAbility != null)
            {
                _mainSlot.UpdateSlotGraphic(_quickBarDataStorage.mainAbility);
            }
            else
            {
                _mainSlot.ClearSlot();
            }

            for (int i = 0; i < _quickBarSlots.Length; i++)
            {
                if (_quickBarDataStorage.TryGetQuickAbility(i, out var ability))
                {
                    _quickBarSlots[i].UpdateSlotGraphic(ability);
                }
                else
                {
                    _quickBarSlots[i].ClearSlot();
                }
            }
        }
    }
}