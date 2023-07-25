using System.Collections;
using System.Collections.Generic;
using Entities;

namespace Leveling
{
	public class ExpForKillsController: IInjectionTarget, IObserver<Entity>
	{
	    [InjectField] ExperienceStorage _storage;

        public bool waitForAllDependencies => false;

        public void AddToObserve(Entity target)
        {
			target.OnDeath += AddExpOnKill;
        }

        public void FinalizeInjection()
        {
        }

        public void RemoveFromObserve(Entity target)
        {
			target.OnDeath -= AddExpOnKill;
        }

        void AddExpOnKill(Entity enemy)
		{
			_storage.AddExp(enemy.expForKill);
		}
	}
}

