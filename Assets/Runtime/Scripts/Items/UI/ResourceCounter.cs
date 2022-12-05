using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Items
{
    public class ResourceCounter : MonoBehaviour
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] Resource _resource;
        [Header("UI Elements")]
        [SerializeField] TextMeshProUGUI _counter;

        private void OnEnable()
        {
            _counter.text = _inventory.resources[_resource.type].ToString();
        }

        public void UpdateResourceValue(ResourceType resourceType)
        {
            if(resourceType != _resource.type) return;
            _counter.text = _inventory.resources[_resource.type].ToString();
        }
    }
}