using System.Collections.Generic;

namespace Effects
{
    public class TemporaryEffectsStorage : AbstractEffectsStorage<TemporaryEffectData>
    {
        List<TemporaryEffectData> _effectsList = new();
        public IEnumerable<TemporaryEffectData> effectsList => _effectsList;

        //UNDONE
        public void AddEffect(IEffectSource source, SourceEffectData effectData)
        {
            var newEffectData = new TemporaryEffectData(effectData);
            base.AddEffect(source, newEffectData);
            _effectsList.Add(newEffectData);
        }
    }
}
