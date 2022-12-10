using UnityEngine.Events;

namespace Map.Objects
{
    public class EmptyActionLogic : IMapActionLogic
    {
        public bool isEnable { get; set; }

        public IIconData template => _template;

        IIconData _template;

        public bool waitForAllDependencies => false;

        public event UnityAction<IMapActionLogic> OnCompletion;

        public EmptyActionLogic(IIconData template)
        {
            _template = template;
        }

        public void DoAction()
        {
            OnCompletion?.Invoke(this);
        }

        public void FinalizeInjection()
        {

        }
    }
}

