using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;

namespace Entities.InteractiveObjects
{
    public class Container : MonoBehaviour, IInteractive
    {
        public void Interact(Player player)
        {
            Debug.Log("Open container");
        }
    }
}