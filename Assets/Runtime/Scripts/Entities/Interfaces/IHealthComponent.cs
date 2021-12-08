using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
	public interface IHealthComponent
	{
		event UnityAction OnHealthChange;
		int currentHealth { get; }
		int maxHealth { get; }
		void DamageHealth(int health);
		void RestoreHealth(int health);
	}
}