using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Effects
{
    public class EffectsStorage : MonoBehaviour, IEntityComponent, IEffectsIterator
    {
        public event UnityAction OnEffectsUpdate;

        StaticEffectsStorage _staticEffectsStorage = new();
        TemporaryEffectsStorage _temporaryEffectsStorage = new();

        public IEnumerable<TemporaryEffectData> temporaryEffects => _temporaryEffectsStorage.effectsList;

        public void AddTemporaryEffect(IEffectSource source, SourceEffectData effectData)
        {
            _temporaryEffectsStorage.AddEffect(source, effectData);
            OnEffectsUpdate?.Invoke();
        }

        public void AddStaticEffect(IEffectSource source, IStaticEffectData effectData)
        {
            _staticEffectsStorage.AddEffect(source, effectData);
            OnEffectsUpdate?.Invoke();
        }

        public IEnumerable<IStaticEffectData> GetEffects(IEffectSignature type)
        {
            foreach (var effect in _staticEffectsStorage.GetEffects(type))
            {
                yield return effect;
            }
            foreach (var effect in _temporaryEffectsStorage.GetEffects(type))
            {
                yield return effect;
            }
        }

        public IEnumerable<IStaticEffectData> GetEffects()
        {
            foreach (var effect in _staticEffectsStorage.GetEffects())
            {
                yield return effect;
            }
            foreach (var effect in _temporaryEffectsStorage.GetEffects())
            {
                yield return effect;
            }
        }
    }
}
