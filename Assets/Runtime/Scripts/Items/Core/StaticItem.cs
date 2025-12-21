namespace Items
{
    public abstract class StaticItem : AbstractItem
    {
        protected abstract StaticItemTemplate _staticTemplate { get; }
        protected override ItemTemplate _template => _staticTemplate;

        public override string displayName => _staticTemplate.displayName;
        public override int value => _staticTemplate.value;
    }
}