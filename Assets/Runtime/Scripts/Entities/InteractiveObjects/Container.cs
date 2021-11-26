using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;

namespace Entities.InteractiveObjects
{
    public class Container : MonoBehaviour, IInteractive
    {
        public void Interact(PlayerCore player)
        {
            Debug.Log("Open container");
        }
    }
}