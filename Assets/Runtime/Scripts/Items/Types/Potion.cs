using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;

namespace Items
{
	[CreateAssetMenu(fileName = "NewPotion", menuName = "Items/Potion")]
	public class Potion : Item, IEffectSource
	{
		[Header("Potion Effects")]
	    [SerializeField] SourceEffectData[] _effects;

        public void AplyEffects(IEffectTarget target)
        {
            foreach (var effect in _effects)
			{
				effect.ApplyEffect(target);
			}
        }
    }
}