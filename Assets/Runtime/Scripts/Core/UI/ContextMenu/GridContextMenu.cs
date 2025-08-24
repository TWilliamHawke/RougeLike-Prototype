using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class GridContextMenu : MonoBehaviour, IContextMenu
    {
        [SerializeField] List<GridContextButon> _buttons;
        [SerializeField] GridLayoutGroup _grid;
        [SerializeField] UIScreen _menu;

        const int COLUMNS = 2;
        const int ROWS = 4;

        bool _gridSizeUpdated = false;

        void Awake()
        {
            foreach (var button in _buttons)
            {
                button.OnClick += CloseMenu;
            }
        }

        //used in editor
        public void OpenMenu()
        {
            _menu.Open();
            UpdateGridSize();
        }

        public void Fill(IEnumerable<ContextActionContainer> actionsList)
        {
            foreach (var button in _buttons)
            {
                button.ClearAction();
            }

            foreach (var action in actionsList)
            {
                int idx = action.preferedPosition - 1;
                idx = Mathf.Clamp(idx, 0, _buttons.Count - 1);
                _buttons[idx].BindAction(action);
            }

        }

        private void CloseMenu()
        {
            _menu.Close();
        }

        private void UpdateGridSize()
        {
            if (_gridSizeUpdated) return;
            _gridSizeUpdated = true;

            var transform = _grid.GetComponent<RectTransform>();
            float totalColumnsWidth = transform.rect.width - _grid.padding.left - _grid.padding.right - _grid.spacing.x;
            float totalRowsHeight = transform.rect.height - _grid.padding.top - _grid.padding.bottom - _grid.spacing.y * (ROWS - 1);

            float cellWidth = totalColumnsWidth / COLUMNS;
            float cellHeight = totalRowsHeight / ROWS;

            _grid.cellSize = new Vector2(cellWidth, cellHeight);
        }
    }
}


