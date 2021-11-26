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
		void ChangeHealth(int health);
	}
}