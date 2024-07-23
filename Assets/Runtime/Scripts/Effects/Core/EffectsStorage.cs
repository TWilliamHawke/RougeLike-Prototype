using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Effects
{
    public class EffectsStorage : MonoBehaviour, IEntityComponent
    {
        public event UnityAction OnEffectsUpdate;

        EffectContainer _mainContainer = new();

        Dictionary<IEffect, TemporaryEffectData> _temporaryEffects = new();
        List<TemporaryEffectData> _temporaryEffectsList = new();

        public IEnumerable<TemporaryEffectData> temporaryEffects => _temporaryEffectsList;

        public void AddTemporaryEffect(SourceEffectData sourceEffectData)
        {
            if (_temporaryEffects.TryGetValue(sourceEffectData.effect, out var effectData))
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

        public void AddStaticEffect(IEffectSource effectSource, SourceEffectData sourceEffectData)
        {
            _mainContainer.AddEffect(effectSource, sourceEffectData);
        }


    }
}
