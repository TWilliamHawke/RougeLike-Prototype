using System.Collections;
using System.Collections.Generic;
using Entities;

namespace Leveling
{
	public class ExpForKillsController: IInjectionTarget
	{
	    [InjectField] ExperienceStorage _storage;

        public bool waitForAllDependencies => false;

        public void AddEnemyToObserve(Entity enemy)
		{
			enemy.OnDeath += AddExpOnKill;
		}

        public void FinalizeInjection()
        {
        }

        void AddExpOnKill(Entity enemy)
		{
			_storage.AddExp(enemy.expForKill);
		}
	}
}

