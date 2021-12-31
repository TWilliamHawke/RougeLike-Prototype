using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
	public interface IHealthComponent
	{
		event UnityAction OnHealthChange;
		int currentHealth { get; }
		int maxHealth { get; }
		Color healthbarColor { get; }
		void DamageHealth(int health);
		void RestoreHealth(int health);
	}
}