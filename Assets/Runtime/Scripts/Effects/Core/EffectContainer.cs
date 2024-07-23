using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Effects
{
    public class EffectContainer
    {
        Dictionary<IEffectSource, IList<IStaticEffectData>> _effectsBySource = new();
        Dictionary<IEffectSignature, IList<EffectSourceDataPair>> _effectsByType = new();


        //UNDONE: Same effects from same source will be replace each other
        //(like armor with additianal armor enchantment)
        public void AddEffect(IEffectSource source, IStaticEffectData effectData)
        {
            AddSource(source, effectData);
            AddType(source, effectData);
        }

        public void AddEffects(IEffectSource source, IList<IStaticEffectData> effects)
        {
            _effectsBySource[source] = effects;
            effects.ForEach(e => AddType(source, e));
        }

        public IEnumerator<IStaticEffectData> GetEffects(IEffectSignature type)
        {
            if (_effectsByType.TryGetValue(type, out var effects))
            {
                foreach (var pair in effects)
                {
                    yield return pair.effectData;
                }
            }
        }

        public IEnumerator<IStaticEffectData> GetEffects()
        {
            foreach (var effectList in _effectsBySource)
            {
                foreach (var effect in effectList.Value)
                {
                    yield return effect;
                }
            }
        }

        public void RemoveEffect(IEffectSource source)
        {
            if (_effectsBySource.Remove(source, out var effects))
            {
                foreach (var effect in effects)
                {
                    if (_effectsByType.TryGetValue(effect.effectType, out var pairs))
                    {
                        for (int i = 0; i < pairs.Count; i++)
                        {
                            if (pairs[i].source == source)
                            {
                                pairs.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }


        private void AddType(IEffectSource source, IStaticEffectData effectData)
        {
            EffectSourceDataPair pair = new(source, effectData);

            if (!_effectsByType.TryGetValue(effectData.effectType, out var pairs))
            {
                pairs = new List<EffectSourceDataPair>() { pair };
                _effectsByType[effectData.effectType] = pairs;
            }
            else
            {
                for (int i = 0; i < pairs.Count; i++)
                {
                    if (pairs[i].source == source)
                    {
                        pairs[i] = pair;
                        return;
                    }
                }
                pairs.Add(pair);
            }
        }

        private void AddSource(IEffectSource source, IStaticEffectData effectData)
        {
            if (!_effectsBySource.TryGetValue(source, out var effects))
            {
                effects = new List<IStaticEffectData>() { effectData };
                _effectsBySource[source] = effects;
            }
            else
            {
                for (int i = 0; i < effects.Count; i++)
                {
                    if (effects[i].effect == effectData.effect)
                    {
                        effects[i] = effectData;
                        return;
                    }
                }
                effects.Add(effectData);
            }
        }


    }

    struct EffectSourceDataPair
    {
        public IEffectSource source { get; init; }
        public IStaticEffectData effectData { get; init; }

        public EffectSourceDataPair(IEffectSource source, IStaticEffectData effectData)
        {
            this.source = source;
            this.effectData = effectData;
        }
    }
}