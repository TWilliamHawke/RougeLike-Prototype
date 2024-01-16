namespace Core.UI
{
    public class OpenModalWindowRadial : OpenModalWindow, IRadialMenuAction
    {
        public RadialButtonPosition preferedPosition { get; init; }
    
        ModalWindowController _modalWindow;
        ModalWindowData _modalWindowData;

        public OpenModalWindowRadial(ModalWindowController modalWindow, ModalWindowData modalWindowData, RadialButtonPosition preferedPosition, string actionTitle) : base(modalWindow, modalWindowData)
        {
            this.preferedPosition = preferedPosition;
            _actionTitle = actionTitle;
        }
    }
}


