using System.Collections;
using System.Collections.Generic;
using Abilities;
using Entities.PlayerScripts;
using Map;

namespace Core.Input
{
    public class DefaultClickActions : IClickActionList, IInjectionTarget
    {
        [InjectField] InputController _inputController;
        [InjectField] TilesGrid _tileGrid;
        [InjectField] Player _player;

        List<IMouseClickAction> _clickActions = new();

        public bool waitForAllDependencies => true;

        void IInjectionTarget.FinalizeInjection()
        {
            FillActionList();
        }

        private void FillActionList()
        {
            //check ui click before tiles
            _clickActions.Add(new ClickUI());
            //check unwalkable before gameobjects
            _clickActions.Add(new ClickUnwalkableTile(
                inputController: _inputController,
                tilemapController: _tileGrid));

            _clickActions.Add(new ClickPlayer(_inputController, _player));

            _clickActions.Add(new ClickRangeAttackTarget(
                inputController: _inputController,
                player: _player.GetComponent<ProjectileController>()));
            _clickActions.Add(new ClickRemoteObject(_inputController, _player));
            _clickActions.Add(new ClickNextTileObject(_inputController, _player));

            //tile hasn't any objects
            _clickActions.Add(new ClickWalkableTile(
                inputController: _inputController,
                tileGrid: _tileGrid,
                player: _player));
        }

        public IEnumerator<IMouseClickAction> GetEnumerator()
        {
            return _clickActions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}