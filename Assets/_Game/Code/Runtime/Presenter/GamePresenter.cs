using MatchHotel.Configuration;
using MatchHotel.Model.Runtime.Model;
using MatchHotel.View;
using UnityEngine;

namespace MatchHotel.Presenter
{
    public class GamePresenter
    {
        private readonly GameModel _gameModel;
        private readonly MenuManager _menuManager;
        private readonly GameView _gameView;
        private LevelPresenter _levelPresenter;
        private LevelView _levelView;

        public GamePresenter(GameView gameView)
        {
            _gameModel = GameModelLocator.Locate();
            _gameView = gameView;
            _menuManager = new MenuManager();
            _menuManager.ChangeMenu(_gameView.LoadingMenuView);
            _menuManager.ChangeMenu(_gameView.LobbyMenuView);
            _gameView.LobbyMenuView.OnPlayRequestedEvent.AddListener(OnPlayRequested);
        }

        private void OnPlayRequested()
        {
            _menuManager.ChangeMenu(_gameView.LoadingMenuView);
            LoadLevel();
        }

        private void LoadLevel()
        {
            _levelView = Object.Instantiate(_gameView.LevelViewPrefab);
            var levelDataArr = GameContext.Instance.levelConfiguration.levels;
            var levelData = levelDataArr[_gameModel.GetCurrentLevelIndex() % levelDataArr.Length];
            _levelPresenter = new LevelPresenter(levelData, _levelView, OnLevelEnd);
            _levelPresenter.StartLevel();
            _menuManager.ChangeMenu(_levelView.GameplayMenu);
            _levelView.GameplayMenu.OnRetryRequestedEvent.AddListener(OnRetryRequested);
            _levelView.GameplayMenu.OnReturnToLobbyRequestedEvent.AddListener(OnReturnToLobbyRequested);
        }
        
        private void DisposeLevel()
        {
            _menuManager.ChangeMenu(_gameView.LoadingMenuView);
            _levelView.GameplayMenu.OnRetryRequestedEvent.RemoveListener(OnRetryRequested);
            _levelView.GameplayMenu.OnReturnToLobbyRequestedEvent.RemoveListener(OnReturnToLobbyRequested);
            _levelPresenter?.Dispose();
            _levelPresenter = null;
            Object.Destroy(_levelView.gameObject);
        }

        private void OnLevelEnd(bool success)
        {
            if (success)
            {
                _gameModel.SetCurrentLevelIndex(_gameModel.GetCurrentLevelIndex() + 1);
            }
        }

        private void OnRetryRequested()
        {
            DisposeLevel();
            LoadLevel();
        }
        
        private void OnReturnToLobbyRequested()
        {
            DisposeLevel();
            _menuManager.ChangeMenu(_gameView.LobbyMenuView);
        }

        public void Dispose()
        {
            _gameView.LobbyMenuView.OnPlayRequestedEvent.RemoveListener(OnPlayRequested);
            _menuManager.Dispose();
            _levelPresenter?.Dispose();
        }
    }
}