using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;

namespace Entities
{
	public abstract class EntityTemplate : ScriptableObject, IDamageSource
	{
		[SerializeField] string _bodyChar = "-";
		[SerializeField] Color _bodyColor = Color.white;
		[SerializeField] int _health;
		[SerializeField] Faction _faction;
		[SerializeField] int _expForKill;

		public int health => _health;
		public string bodyChar => _bodyChar;
		public Color bodyColor => _bodyColor;
		public int expForKill => _expForKill;
		public Faction faction => _faction;

        public abstract int minDamage { get; }
        public abstract int maxDamage { get; }
        public abstract DamageType damageType { get; }
    }
}


