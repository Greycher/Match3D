using System;
using MatchHotel.Presenter;
using UnityEngine;

namespace MatchHotel.View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private LoadingMenuView loadingMenuView;
        [SerializeField] private LobbyMenuView lobbyMenuView;
        [SerializeField] private LevelView levelViewPrefab;
        
        private GamePresenter _presenter;
        public LoadingMenuView LoadingMenuView => loadingMenuView;
        public LobbyMenuView LobbyMenuView => lobbyMenuView;

        public LevelView LevelViewPrefab => levelViewPrefab;

        private void Awake()
        {
            _presenter = new GamePresenter(this);
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
        }
    }
}