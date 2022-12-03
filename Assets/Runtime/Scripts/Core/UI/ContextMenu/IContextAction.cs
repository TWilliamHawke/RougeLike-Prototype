using UnityEngine;

namespace Core
{
    public interface IContextAction
	{
		string actionTitle { get; }
		void DoAction()
		{
			Debug.Log(actionTitle);
		}
	}
}


