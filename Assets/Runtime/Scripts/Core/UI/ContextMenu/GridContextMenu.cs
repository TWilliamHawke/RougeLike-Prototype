using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class GridContextMenu : MonoBehaviour, IContextMenu
    {
        [SerializeField] List<GridContextButon> _buttons;
        [SerializeField] GridLayoutGroup _grid;

        const int COLUMNS = 2;
        const int ROWS = 4;

        void Awake()
        {
            var transform = _grid.GetComponent<RectTransform>();
            float totalColumnsWidth = transform.rect.width - _grid.padding.left - _grid.padding.right - _grid.spacing.x;
            float totalRowsHeight = transform.rect.height - _grid.padding.top - _grid.padding.bottom - _grid.spacing.y * (ROWS - 1);

            float cellWidth = totalColumnsWidth / COLUMNS;
            float cellHeight = totalRowsHeight / ROWS;

            _grid.cellSize = new Vector2(cellWidth, cellHeight);
        }

        public void Fill(IEnumerable<ContextActionContainer> actionsList)
        {
            foreach (var button in _buttons)
            {
                button.ClearAction();
            }

            foreach (var action in actionsList)
            {
                int idx = Mathf.Clamp(action.preferedPosition, 0, _buttons.Count - 1);
                _buttons[idx].BindAction(action);
            }

        }
    }
}


