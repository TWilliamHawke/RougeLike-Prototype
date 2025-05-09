using UnityEngine;

namespace Abilities
{
    public class ProjectileAbility : IAbility
    {
        public Sprite abilityIcon => throw new System.NotImplementedException();

        ProjectileAbilityTemplate _template;

        public ProjectileAbility(ProjectileAbilityTemplate template)
        {
            _template = template;
        }
    }
}