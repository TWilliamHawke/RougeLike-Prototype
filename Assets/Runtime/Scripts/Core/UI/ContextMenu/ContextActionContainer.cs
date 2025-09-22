namespace Core
{
    public abstract class ContextActionContainer : IContextAction
    {
        public string actionTitle => _actionTemplate.actionTitle;
        public int preferedPosition => _actionTemplate.preferedPosition;
        public bool closeBackgroundScreen => _actionTemplate.closeBackgroundScreen;

        ContextActionTemplate _actionTemplate;

        public abstract void DoAction();

        public void SetActionTemplate(ContextActionTemplate template)
        {
            _actionTemplate = template;
        }
    }
}


