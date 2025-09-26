namespace Items
{
    public class MagicScroll : AbstractItem
    {
        public override int value => _magicScrollTemplate.value;

        protected override IItemTemplate _template => _magicScrollTemplate;

        MagicScrollTemplate _magicScrollTemplate;

        public MagicScroll(MagicScrollTemplate magicScrollTemplate)
        {
            _magicScrollTemplate = magicScrollTemplate;
        }

        public override string GetDescription()
        {
            return _magicScrollTemplate.GetDescription();
        }
    }
}