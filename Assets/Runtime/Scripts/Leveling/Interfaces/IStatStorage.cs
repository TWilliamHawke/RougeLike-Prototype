using System.Collections.Generic;
using Effects;

namespace Entities.Stats
{
    public interface IStatStorage
    {
        void InitStat(StaticStat stat, int baseValue);
        void AddObserver(IObserver<StaticStatStorage> observer, StaticStat stat);
    }
}
