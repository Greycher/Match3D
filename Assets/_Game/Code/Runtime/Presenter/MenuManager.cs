using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace MatchHotel.View
{
    public class MenuManager
    {
        private MenuView _activeMenuView;
        private Queue<IMenuCommand> _taskQueue = new();
        private bool _executingCommand = false;
        private EventSystem _eventSystem;

        public void ChangeMenu(MenuView menuView)
        {
            if (_activeMenuView)
            {
                _taskQueue.Enqueue(new CloseMenuCommand(_activeMenuView, OnCloseTaskCompleted));
            }
            _taskQueue.Enqueue(new OpenMenuCommand(menuView, OnOpenTaskCompleted));
            if (!_executingCommand)
            {
                ConsumeTask();
            }
        }

        private void ConsumeTask()
        {
            if (_taskQueue.Count > 0)
            {
                DisableInput();
                _executingCommand = true;
                _taskQueue.Dequeue().Execute();
            }
        }
        
        private void OnOpenTaskCompleted(MenuView menuView)
        {
            _activeMenuView = menuView;
            OnTaskCompleted();
        }
        
        private void OnCloseTaskCompleted(MenuView menuView)
        {
            _activeMenuView = null;
            OnTaskCompleted();
        }

        private void OnTaskCompleted()
        {
            EnableInput();
            _executingCommand = false;
            ConsumeTask();
        }

        private void DisableInput()
        {
            _eventSystem = EventSystem.current;
            _eventSystem.enabled = false;
        }
        
        private void EnableInput()
        {
            _eventSystem.enabled = true;
        }

        public void Dispose() {}
    }
}