using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
	public interface IHealthComponent : IEntityComponent
	{
        bool isDead { get; }

        void DamageHealth(int health);
		void RestoreHealth(int health);
	}
}