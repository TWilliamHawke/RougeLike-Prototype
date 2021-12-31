using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Entities.Player
{
	[CreateAssetMenu(fileName ="ActiveAbilities", menuName ="Musc/ActiveAbilities")]
	public class ActiveAbilities : ScriptableObject
	{
	    IAbilitySource[] _activeAbilities = new IAbilitySource[10];

		AbilityController _playerController;

		public IAbilitySource this[int index]
		{
			get => index <= 10 ? _activeAbilities[index] : null;
			set => _activeAbilities[index] = value;
		}

		public void Init(AbilityController abilityController)
		{
			_playerController = abilityController;
		}

		public void UseAbility(int index)
		{
			if(index >= 10 || _activeAbilities[index] == null)  return;

			_activeAbilities[index].UseAbility(_playerController);
		}

		public void RemoveAbilityAt(int index)
		{
			
		}

	}
}