using System;

namespace MatchHotel.View
{
    public class CloseMenuCommand : IMenuCommand
    {
        private readonly MenuView _menuView;
        private readonly Action<MenuView> _onComplete;

        public CloseMenuCommand(MenuView menuView, Action<MenuView> onComplete)
        {
            _onComplete = onComplete;
            _menuView = menuView;
        }

        public void Execute()
        {
            _menuView.Close(_onComplete);
        }
    }
}