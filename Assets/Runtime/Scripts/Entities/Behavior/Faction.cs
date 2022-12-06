using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Behavior
{
    [CreateAssetMenu(fileName = "Faction", menuName = "Entities/Faction", order = 0)]
    public class Faction : ScriptableObject
    {
        [SerializeField] bool _attackPlayer;
        [SerializeField] bool _attackAnyOtherFactions;

        [HideInInspector]
        [SerializeField] bool _isPlayerFaction;

        [SerializeField] Faction[] _enemyFactions;

        HashSet<Faction> _enemyFactionsSet = new();

		public bool isPlayerFaction => _isPlayerFaction;

        public bool IsAgressiveToward(Faction other)
        {
			if (this == other) return false;
            if (_attackPlayer && other._isPlayerFaction) return true;
			if (_attackAnyOtherFactions || other._attackAnyOtherFactions) return true;
			if (FactionIsEnemy(other) || other.FactionIsEnemy(this)) return true;


            return false;
        }

		public BehaviorType GetAntiPlayerBehavior()
		{
			if (_isPlayerFaction) return BehaviorType.none;
			if (_attackPlayer) return BehaviorType.agressive;
			return BehaviorType.neutral;
		}

		bool FactionIsEnemy(Faction faction)
		{
			foreach (var enemy in _enemyFactions)
			{
				if(enemy == faction) return true;
			}
			return false;
		}
    }
}


