using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Input;

namespace Core.UI
{
    public class TileInfoPanel : MonoBehaviour, IUIScreen
    {
        [SerializeField] InputController _inputController;
		[SerializeField] Text _infoText;

        void OnDestroy()
        {
			_inputController.OnHoveredTileChange -= UpdateText;
        }

        public void Init()
        {
			_inputController.OnHoveredTileChange += UpdateText;

        }

		void UpdateText(Vector3Int tilePosition)
		{
			_infoText.text = $"[x:{tilePosition.x}, y:{tilePosition.y}]";
		}


    }
}