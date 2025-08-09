using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Abilities
{
    public abstract class AbilityButton : MonoBehaviour, IAbilityCounterHandler
    {
        [SerializeField] Image _abilityIcon;
        [SerializeField] LayoutGroup _abilityCount;
        [SerializeField] TextMeshProUGUI _abilityCountText;

        public void UpdateButtonGraphic(IAbilityContainerData data)
        {
            _abilityIcon.gameObject.SetActive(true);
            _abilityIcon.sprite = data.icon;
            data.UpdateAbilityButton(this);
        }

        protected void HideIcon()
        {
            _abilityIcon.gameObject.SetActive(false);
        }

        public void HideAbilityCounter()
        {
            _abilityCountText.text = "";
            _abilityCount.Disable();
        }

        public void ShowAbilityCounter(int numOfUses)
        {
            _abilityCount.Enable();
            _abilityCountText.text = numOfUses < 100 ? numOfUses.ToString() : "99+";
        }
    }
}