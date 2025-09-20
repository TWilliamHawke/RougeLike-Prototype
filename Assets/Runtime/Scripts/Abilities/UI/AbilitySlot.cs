using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Abilities
{
    [RequireComponent(typeof(AbilityButton))]
    public class AbilitySlot : UIDataElement<IAbilityContainer>, IPointerClickHandler
    {
        [SerializeField] TextMeshProUGUI _abilityName;
        [SerializeField] AbilityButton _abilityButton;

        public event UnityAction<IAbilityContainer> OnAbilitySelected;

        IAbilityContainer _abilityContainer;

        public override void BindData(IAbilityContainer data)
        {
            _abilityContainer = data;
            _abilityButton.UpdateButtonGraphic(data);
            _abilityName.text = data.displayName;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnAbilitySelected?.Invoke(_abilityContainer);
        }
    }
}