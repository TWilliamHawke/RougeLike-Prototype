using System.Collections.Generic;

namespace Localisation
{
    public interface ILocalisationLoader
    {
        Dictionary<string, string> CreateDictionary();
    }
}