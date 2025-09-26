namespace Items
{
    public class Jevelry : AbstractItem
    {
        public override int value => _jevelryTemplate.value;

        protected override IItemTemplate _template => _jevelryTemplate;

        JevelryTemplate _jevelryTemplate;

        public Jevelry(JevelryTemplate jevelryTemplate)
        {
            _jevelryTemplate = jevelryTemplate;
        }

        public override string GetDescription()
        {
            return _jevelryTemplate.GetDescription();
        }
    }
}