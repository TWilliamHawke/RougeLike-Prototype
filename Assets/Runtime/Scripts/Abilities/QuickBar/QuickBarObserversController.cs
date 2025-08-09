using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class QuickBarObserversController : MonoBehaviour
    {
        [SerializeField] QuickBarDataStorage _quickBarDataStorage;

        List<IObserver<IAbilityContainer>> _observers = new();
        bool _isAwaken = false;

        void Awake()
        {
            _isAwaken = true;
            _quickBarDataStorage.OnAbilityAdded += AddAbilityToObservers;
            _quickBarDataStorage.OnAbilityRemoved += RemoveAbilityFromObservers;

            _observers.ForEach(observer => ObserveSlots(observer));
        }

        void OnDestroy()
        {
            _quickBarDataStorage.OnAbilityAdded -= AddAbilityToObservers;
            _quickBarDataStorage.OnAbilityRemoved -= RemoveAbilityFromObservers;
        }

        public void AddSlotObserver(IObserver<IAbilityContainer> observer)
        {
            _observers.Add(observer);
            if (!_isAwaken) return;
            ObserveSlots(observer);
        }

        public void RemoveSlotObserver(IObserver<IAbilityContainer> observer)
        {
            _observers.Remove(observer);
            _quickBarDataStorage.ForEach(ability => observer.RemoveFromObserve(ability));
        }

        private void ObserveSlots(IObserver<IAbilityContainer> observer)
        {
            _quickBarDataStorage.ForEach(ability => observer.AddToObserve(ability));
        }

        private void AddAbilityToObservers(IAbilityContainer ability)
        {
            _observers.ForEach(observer => observer.AddToObserve(ability));
        }

        private void RemoveAbilityFromObservers(IAbilityContainer ability)
        {
            _observers.ForEach(observer => observer.RemoveFromObserve(ability));
        }
    }
}
