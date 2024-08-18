using System.Collections;
using System.Collections.Generic;
using Items;
using Magic;
using TMPro;
using UI.DragAndDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{

    public class RadialContextButton : MonoBehaviour, IDropTarget<IContextMenuData>
    {
        [SerializeField] RadialButtonPosition _buttonPosition;
        [SerializeField] TextMeshProUGUI _buttonText;
        [SerializeField] Image _coloredMask;
        [SerializeField] Image _visibleMask;
        [SerializeField] bool _checkAlpha = true;
        [SerializeField] Color _defaultTextColor = Color.black;
        [SerializeField] Color _highlightTextColor = Color.white;
        [SerializeField] Color _buttonColor = Color.yellow;
        [SerializeField] Color _inactiveColor = Color.gray;
        [SerializeField] Image[] _visibleFrameParts;
        [SerializeField] Image[] _coloredFrameParts;

        IContextAction _buttonAction;
        public bool checkImageAlpha => _checkAlpha;

        public RadialButtonPosition buttonPosition => _buttonPosition;

        void Start()
        {
            _coloredMask.color = _buttonColor;
        }

        public bool DataIsMeet(IContextMenuData _)
        {
            return _buttonPosition == RadialButtonPosition.middle || _buttonAction is not null;
        }

        public void DropData(IContextMenuData _)
        {
            _buttonAction?.DoAction();
        }

        public void Highlight()
        {
            _visibleMask.Enable();
            _buttonText.color = _highlightTextColor;
            _visibleFrameParts.ForEach(framepart => framepart.Disable());
        }

        public void UnHighlight()
        {
            _visibleMask.Disable();
            _buttonText.color = _defaultTextColor;
            _visibleFrameParts.ForEach(framepart => framepart.Enable());
        }

        public void BindAction(IContextAction action)
        {
            _coloredFrameParts.ForEach(framepart => framepart.color = _buttonColor);
            if (_buttonPosition == RadialButtonPosition.middle) return;
            _buttonAction = action;
            _buttonText.text = action.actionTitle;
        }

        public void ClearAction()
        {
            _coloredFrameParts.ForEach(framepart => framepart.color = _inactiveColor);
            if (_buttonPosition == RadialButtonPosition.middle) return;
            _buttonAction = null;
            _buttonText.text = "";
        }
    }

    public interface IContextMenuData
    {
    }
}


