using UnityEngine;

namespace Map.UI
{
    public class TaskPanelClickObserver : MonoBehaviour
    {
        [SerializeField] TaskPanel _taskPanel;
        [SerializeField] UIScreen _screen;

        void Awake()
        {
            _taskPanel.OnPanelClick += CloseScreen;
        }

        private void CloseScreen()
        {
            _screen.Close();
        }
    }
}

