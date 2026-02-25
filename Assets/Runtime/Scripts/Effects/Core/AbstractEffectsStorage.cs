using System.Collections.Generic;

namespace Effects
{
    public class AbstractEffectsStorage<T> : IEffectsIterator where T : IStaticEffectData
    {
        Dictionary<IEffectSource, IList<T>> _effectsBySource = new();
        Dictionary<IEffectSignature, IList<EffectSourceDataPair<T>>> _effectsByType = new();

        public IEnumerable<IStaticEffectData> GetEffects(IEffectSignature type)
        {
            if (_effectsByType.TryGetValue(type, out var effects))
            {
                foreach (var pair in effects)
                {
                    yield return pair.effectData;
                }
            }
        }

        public IEnumerable<IStaticEffectData> GetEffects()
        {
            foreach (var effectList in _effectsBySource)
            {
                foreach (var effect in effectList.Value)
                {
                    yield return effect;
                }
            }
        }

        //UNDONE: Same effects from same source will be replace each other
        //(like armor with additianal armor enchantment)
        protected void AddEffect(IEffectSource source, T effectData)
        {
            AddSource(source, effectData);
            AddType(source, effectData);
        }

        private void AddType(IEffectSource source, T effectData)
        {
            EffectSourceDataPair<T> pair = new(source, effectData);

            if (!_effectsByType.TryGetValue(effectData.effectType, out var pairs))
            {
                pairs = new List<EffectSourceDataPair<T>>() { pair };
                _effectsByType[effectData.effectType] = pairs;
            }
            else
            {
                for (int i = 0; i < pairs.Count; i++)
                {
                    if (pairs[i].HasSameComponents(pair))
                    {
                        pairs[i] = pair;
                        return;
                    }
                }
                pairs.Add(pair);
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
                            if (pairs[i].source.Equals(source))
                            {
                                pairs.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }

        private void AddSource(IEffectSource source, T effectData)
        {
            if (!_effectsBySource.TryGetValue(source, out var effects))
            {
                effects = new List<T>() { effectData };
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

    struct EffectSourceDataPair<T> where T : IStaticEffectData
    {
        public IEffectSource source { get; init; }
        public T effectData { get; init; }

        public EffectSourceDataPair(IEffectSource source, T effectData)
        {
            this.source = source;
            this.effectData = effectData;
        }

        public bool HasSameComponents(EffectSourceDataPair<T> other)
        {
            return other.source == source && other.effectData.effect == effectData.effect;
        }
    }

}