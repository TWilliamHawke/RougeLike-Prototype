using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Stats
{
    public class StatList : ScriptableObject
    {
        [Header("Capped Stats")]
        public StoredResource health;
        public StoredResource mana;
        [Space()]
        [Header("Static Stat")]
        public StaticStat maxHealth;
        public StaticStat maxMana;
    }
}
