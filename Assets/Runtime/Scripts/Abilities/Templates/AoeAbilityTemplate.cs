using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/AOE")]
    public class AoeAbilityTemplate : AbilityTemplate
    {
        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }

        public override void SelectAbilityController(AbilityController controller)
        {
            throw new System.NotImplementedException();
        }
    }
}