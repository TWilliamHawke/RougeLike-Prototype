using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class OpenScreen : IContextAction
    {
        public string actionTitle => _actionTitle;

		string _actionTitle;
		UIScreen _screen;

        public OpenScreen(UIScreen screen, string actionTitle = "Open Screen")
        {
            _actionTitle = actionTitle;
            _screen = screen;
        }

		public void DoAction()
		{
			_screen.Open();
		}
    }
}


