using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Effects
{
	public class EffectStorage
	{
		public event UnityAction OnEffectsUpdate;

		IEffectTarget _self;

        List<StaticEffectData> _staticEffects = new();
		List<TemporaryEffectData> _temporaryEffectsList = new();
		Dictionary<Effect, TemporaryEffectData> _temporaryEffects = new();

		public List<TemporaryEffectData> temporaryEffects => _temporaryEffectsList;

        public EffectStorage(IEffectTarget self)
        {
            _self = self;
        }

        public EffectStorage()
        {
        }

		public void SetEffectTarget(IEffectTarget self)
		{
			_self = self;
		}

		public void AddTemporaryEffect(SourceEffectData sourceEffectData)
		{
			if(_temporaryEffects.TryGetValue(sourceEffectData.effect, out var effectData))
			{
				effectData.UpdateEffectData(sourceEffectData);
			}
			else
			{
				var newEffectData = new TemporaryEffectData(sourceEffectData);
				_temporaryEffects[sourceEffectData.effect] = newEffectData;
				_temporaryEffectsList.Add(newEffectData);
			}

			OnEffectsUpdate?.Invoke();
		}

		public void AddStaticEffect(SourceEffectData sourceEffectData)
		{
			
		}

		
		
	}
}