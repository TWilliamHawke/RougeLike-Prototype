using Entities.Stats;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Restoration", menuName = "Effects/ResourceRestoration")]
    public class IncreaceStoredResource : ChangeStoredResource
    {
        protected override void ChangeResource(ResourceContainer container, int value)
        {
            container.ChangeStat(value);
        }
    }

}
