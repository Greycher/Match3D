using System;

namespace MatchHotel.View
{
    public class OpenMenuCommand : IMenuCommand
    {
        private readonly MenuView _menuView;
        private readonly Action<MenuView> _onComplete;

        public OpenMenuCommand(MenuView menuView, Action<MenuView> onComplete)
        {
            _onComplete = onComplete;
            _menuView = menuView;
        }

        public void Execute()
        {
            _menuView.Open(_onComplete);
        }
    }
}