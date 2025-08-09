using UnityEngine;

namespace Abilities
{
    public class QuickBar : MonoBehaviour
    {
        [SerializeField] QuickBarSlot[] _quickBarSlots;
        [SerializeField] QuickBarSlot _mainSlot;
        [SerializeField] QuickBarDataStorage _quickBarDataStorage;

        void Awake()
        {
            SetUpSlotNumbers();
            UpdateSlots();
            _quickBarDataStorage.OnQuickBarChange += UpdateSlots;
        }

        void OnDestroy()
        {
            _quickBarDataStorage.OnQuickBarChange -= UpdateSlots;
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
            _mainSlot.ClearSlot();
            if (_quickBarDataStorage.mainAbility != null)
            {
                _mainSlot.UpdateButtonGraphic(_quickBarDataStorage.mainAbility);
            }

            for (int i = 0; i < _quickBarSlots.Length; i++)
            {
                _quickBarSlots[i].ClearSlot();
                if (_quickBarDataStorage.TryGetQuickAbility(i, out var ability))
                {
                    _quickBarSlots[i].UpdateButtonGraphic(ability);
                }
            }
        }
    }
}