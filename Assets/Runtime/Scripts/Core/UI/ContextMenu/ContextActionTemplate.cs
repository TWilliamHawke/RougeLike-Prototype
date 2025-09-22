using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "ContextActionTemplate", menuName = "Musc/ContextActionTemplate")]
    public class ContextActionTemplate : ScriptableObject
    {
        [SerializeField] LocalString _actionTitle = "Action title";
        [SerializeField] int _preferedPosition = 99;
        [SerializeField] Color _buttonColor;
        [SerializeField] Color _borderColor = Color.green;
        [SerializeField] Color _textColor = Color.black;
        [SerializeField] bool _closeActiveScreen = true;

        public int preferedPosition => _preferedPosition;
        public string actionTitle => _actionTitle;
        public bool closeBackgroundScreen => _closeActiveScreen;

    }
}
