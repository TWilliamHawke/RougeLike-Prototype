using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public interface IUIScreen
	{
	    GameObject gameObject { get; }
		void Init();
	}
