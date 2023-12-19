using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Entities.Stats;
using UnityEngine;

namespace Entities
{
	public abstract class EntityTemplate : ScriptableObject, ITemplateWithBaseStats, IEntityTemplate,
	 IDamageSource
	{
		[SerializeField] string _bodyChar = "-";
		[SerializeField] Color _bodyColor = Color.white;
		[SerializeField] int _health;
		[SerializeField] Faction _faction;
		[SerializeField] int _expForKill;
        [SerializeField] StatValues _statValues;

		public int health => _health;
		public string bodyChar => _bodyChar;
		public Color bodyColor => _bodyColor;
		public int expForKill => _expForKill;
		public Faction faction => _faction;

        public abstract int minDamage { get; }
        public abstract int maxDamage { get; }
        public abstract DamageType damageType { get; }
		
		public abstract Entity CreateEntity(EntitiesSpawner spawner, Vector3 position);

        public void InitStats(IStatContainer container)
        {
            _statValues.InitStats(container);
        }
    }

	public interface ITemplateWithBaseStats
	{
		int health { get; }
		string bodyChar { get; }
		Color bodyColor { get; }
		int expForKill { get; }
		Faction faction { get; }
        void InitStats(IStatContainer container);
	}

	public interface IEntityTemplate
	{
		Entity CreateEntity(EntitiesSpawner spawner, Vector3 position);
	}
}


