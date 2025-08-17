using System.Collections;
using System.Collections.Generic;
using Entities.Stats;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Effects/Damage Type")]
    public class DamageStoredResource : ChangeStoredResource
    {
        [SerializeField] StaticStat _resist;

        protected override void ChangeResource(ResourceContainer container, int value)
        {
            container.ChangeStat(-value);
        }
    }

}
