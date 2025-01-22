using MatchHotel.Model.Runtime.Model;
using MatchHotel.View;

namespace MatchHotel.Presenter
{
    public class LevelNumberPresenter
    {
        private readonly LevelNumberView _view;
        private readonly GameModel _gameModel;

        public LevelNumberPresenter(LevelNumberView view)
        {
            _gameModel = GameModelLocator.Locate();
            _view = view;
            _gameModel.OnLevelIndexChangeEvent += OnLevelIndexChange;
            UpdateView(_gameModel.GetCurrentLevelIndex());
        }

        private void OnLevelIndexChange(int levelIndex)
        {
            UpdateView(levelIndex);
        }

        private void UpdateView(int levelIndex)
        {
            _view.UpdateView(levelIndex + 1);
        }

        public void Dispose()
        {
            _gameModel.OnLevelIndexChangeEvent -= OnLevelIndexChange;
        }
    }
}