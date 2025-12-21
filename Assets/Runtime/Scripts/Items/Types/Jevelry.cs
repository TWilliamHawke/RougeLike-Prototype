namespace Items
{
    public class Jevelry : StaticItem
    {
        public override int value => _jevelryTemplate.value;

        protected override StaticItemTemplate _staticTemplate => _jevelryTemplate;

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