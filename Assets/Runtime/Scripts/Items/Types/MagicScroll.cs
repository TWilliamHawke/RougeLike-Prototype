namespace Items
{
    public class MagicScroll : StaticItem
    {
        public override int value => _magicScrollTemplate.value;

         protected override StaticItemTemplate _staticTemplate => _magicScrollTemplate;

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