using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    public class DefaultMapActionsController : IMapActionsController
    {
		List<IMapActionLogic> _actionsLogic = new List<IMapActionLogic>();

        public event UnityAction OnActionStateChange;

        public IMapActionLogic this[int idx] => _actionsLogic[idx];
        public int count => _actionsLogic.Count;

        public void AddLogic(IActionLogicCreator actionLogicCreator)
        {
            AddLogic(actionLogicCreator.CreateActionLogic());
        }

        public void AddLogic(IMapActionLogic actionLogic)
		{
			_actionsLogic.Add(actionLogic);
			actionLogic.OnCompletion += DisableAction;
		}

		private void DisableAction(IMapActionLogic actionLogic)
		{
			actionLogic.isEnable = false;
			OnActionStateChange?.Invoke();
		}
    }
}

