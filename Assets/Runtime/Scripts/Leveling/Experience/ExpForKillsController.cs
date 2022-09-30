using System.Collections;
using System.Collections.Generic;
using Entities;

namespace Leveling
{
	public class ExpForKillsController
	{
	    ExperienceStorage _storage;

        public ExpForKillsController(ExperienceStorage storage)
        {
            _storage = storage;
        }

        public void AddEnemyToObserve(Enemy enemy)
		{
			enemy.OnDeath += AddExpOnKill;
		}

		void AddExpOnKill(Enemy enemy)
		{
			_storage.AddExp(enemy.expForKill);
		}
	}
}

