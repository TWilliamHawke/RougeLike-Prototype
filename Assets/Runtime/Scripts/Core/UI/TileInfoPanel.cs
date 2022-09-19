using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Input;

namespace Core.UI
{
    public class TileInfoPanel : MonoBehaviour, IUIScreen, IInjectionTarget
    {
        [InjectField] InputController _inputController;
        
        [SerializeField] Injector _inputControllerInjector;
		[SerializeField] Text _infoText;

        bool IInjectionTarget.waitForAllDependencies => false;

        void OnDestroy()
        {
			_inputController.OnHoveredTileChange -= UpdateText;
        }

        public void Init()
        {
            _inputControllerInjector.AddInjectionTarget(this);
        }

		void UpdateText(Vector3Int tilePosition)
		{
			_infoText.text = $"[x:{tilePosition.x}, y:{tilePosition.y}]";
		}

        public void FinalizeInjection()
        {
			_inputController.OnHoveredTileChange += UpdateText;
        }
    }
}