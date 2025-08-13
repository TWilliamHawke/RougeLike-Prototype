using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Input;
using Map;

namespace Core.UI
{
    public class TileInfoPanel : MonoBehaviour
    {
        [InjectField] HoveredTileObserver _inputController;
        
		[SerializeField] Text _infoText;

        void OnDestroy()
        {
			_inputController.OnHoveredTileChange -= UpdateText;
        }

		void UpdateText(TileNode node)
		{
			_infoText.text = $"[x:{node.x}, y:{node.y}]";
		}

        public void FinalizeInjection()
        {
			_inputController.OnHoveredTileChange += UpdateText;
        }
    }
}