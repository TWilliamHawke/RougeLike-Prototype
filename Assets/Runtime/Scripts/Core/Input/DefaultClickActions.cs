using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Abilities;
using Entities.PlayerScripts;
using Map;

namespace Core.Input
{
    public class DefaultClickActions : IClickActionList, IInjectionTarget
    {
        [InjectField] Player _player;

        QuickBarDataStorage _quickBarDataStorage { get; init; }
        List<IClickAction> _clickActions = new();

        public bool waitForAllDependencies => true;

        public DefaultClickActions(QuickBarDataStorage quickBarDataStorage)
        {
            _quickBarDataStorage = quickBarDataStorage;
            _quickBarDataStorage.OnInit += FillActionList;
            _quickBarDataStorage.OnMainAbilityChanged += FillActionList;
        }

        public void CleanUp()
        {
            _quickBarDataStorage.OnInit -= FillActionList;
            _quickBarDataStorage.OnMainAbilityChanged -= FillActionList;
        }

        void IInjectionTarget.FinalizeInjection()
        {
            FillActionList();
        }

        private void FillActionList()
        {
            if(_quickBarDataStorage.movementAbility == null) return;
            if (_player == null) return;
            _clickActions.Clear();

            //check ui click before tiles
            _clickActions.Add(new ClickUI());
            _clickActions.Add(new ClickNextTileObject(_player));
            _clickActions.Add(new ClickPlayer(_player));
            _clickActions.Add(new ClickAbilityTarget(_quickBarDataStorage.mainAbility));
            //tile hasn't any objects
            _clickActions.Add(new ClickAbilityTarget(_quickBarDataStorage.movementAbility));
            _clickActions.Add(new ClickUnwalkableTile());

        }

        public IEnumerator<IClickAction> GetEnumerator()
        {
            return _clickActions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}