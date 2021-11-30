using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
	public interface IHealthComponent
	{
		event UnityAction OnHealthChange;
		Transform transform { get; }
		int currentHealth { get; }
		int maxHealth { get; }
		void DecreaseHealth(int health);
		void IncreaseHealth(int health);
	}
}