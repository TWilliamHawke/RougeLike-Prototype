using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "WeaponAbilityList", menuName = "Abilities/Lists/WeaponAbilityList")]
    public class WeaponAbilityList : ScriptableObject
    {
        [SerializeField] AbilityTemplate _baseAbility;
        [SerializeField] AbilityTemplate[] _skillBasedAbilities;

        public AbilityTemplate baseAbility => _baseAbility;

    }
}