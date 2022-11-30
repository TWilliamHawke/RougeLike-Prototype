using UnityEngine;

namespace Core
{
	public interface IContextAction
	{
		RadialButtonPosition preferedPosition { get; }
		string actionTitle { get; }
		void DoAction()
		{
			Debug.Log(actionTitle);
		}
	}
}


