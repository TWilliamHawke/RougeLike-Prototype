using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
	public interface IHaveHealth
	{
		event UnityAction<int> OnHealthChange;
		Transform transform { get; }
		int currentHealth { get; }
		int maxHealth { get; }
		void ChangeHealth(int health);
	}
}