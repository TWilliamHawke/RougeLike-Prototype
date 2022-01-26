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

        bool _isSubscribed;

        private void OnEnable()
        {
            _counter.text = _inventory.resources[_resource.type].ToString();
            TrySubscribe();
        }

        void TrySubscribe()
        {
            if(_isSubscribed) return;

            _inventory.resources.OnResourceChange+=UpdateResourceValue;
            _isSubscribed = true;
        }

        void UpdateResourceValue(ResourceType resourceType)
        {
            if(resourceType != _resource.type) return;
            _counter.text = _inventory.resources[_resource.type].ToString();
        }
    }
}