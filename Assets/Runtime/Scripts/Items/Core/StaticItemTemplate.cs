using UnityEngine;

namespace Items
{
    public abstract class StaticItemTemplate : ItemTemplate, IItemTemplate
    {
        [UseFileName] [Space(order =2 )]
        [SerializeField] string _displayName;
        [UseFileName] [Space(order = 1)]
        [SerializeField] int _value;

        public virtual int value => _value;
        public virtual string displayName => _displayName;

        public abstract string GetDescription();
    }
}