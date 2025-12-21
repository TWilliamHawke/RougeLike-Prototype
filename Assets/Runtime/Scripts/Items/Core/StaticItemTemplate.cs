using UnityEngine;

namespace Items
{
    public abstract class StaticItemTemplate : ItemTemplate, IItemTemplate
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [UseFileName]
        [SerializeField] int _value;

        public virtual int value => _value;
        public virtual string displayName => _displayName;

        public abstract string GetDescription();
    }
}