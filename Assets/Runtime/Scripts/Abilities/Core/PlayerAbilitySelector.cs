using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

namespace Abilities
{
    public class PlayerAbilitySelector : MonoBehaviour
    {
        [SerializeField] QuickBarDataStorage _quickBarDataStorage;

        IAbilityContainer _defaultAbility => _quickBarDataStorage.mainAbility;
        IAbilityContainer _defaultMovementAbility;

        IAbilityContainer _selectedAbility;

        public void SelectAbility(IAbilityContainer ability)
        {
            _selectedAbility = ability;
        }

        public void CancelSelection()
        {
            _selectedAbility = _defaultAbility;
        }

        public void UseMovementAbility(TileNode tileNode)
        {
            
        }
    }
}
