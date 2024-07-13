using System.Collections;
using System.Collections.Generic;
using Entities.Stats;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Damage", menuName = "Effects/Damage Type")]
    public class ResourceChangeFactor : ScriptableObject
    {
        [LocalisationKey]
        [SerializeField] string _displayName;
        [LocalisationKey]
        [SerializeField] string _description;
        [SpritePreview]
        [SerializeField] Sprite _icon;

        [SerializeField] StoredResource _damagedStat;
        [SerializeField] StaticStat _resist;

        [SerializeField] StaticStat[] _damageMods;
    }
}
