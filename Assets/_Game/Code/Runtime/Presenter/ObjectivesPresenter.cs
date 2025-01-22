using MatchHotel.Configuration;
using MatchHotel.Model;
using MatchHotel.View;

namespace MatchHotel.Presenter
{
    public class ObjectivesPresenter
    {
        private readonly ObjectivesModel _objectivesModel;
        private readonly ObjectivesView _objectivesView;

        public ObjectivesPresenter(ObjectivesModel objectivesModel, ObjectivesView objectivesView)
        {
            _objectivesModel = objectivesModel;
            _objectivesView = objectivesView;
            InitializeView();
            _objectivesModel.OnObjectiveUpdateEvent += OnObjectiveUpdate;
        }

        private void OnObjectiveUpdate(int itemID, int remainingCount)
        {
            _objectivesView.UpdateObjective(itemID, remainingCount);
        }

        private void InitializeView()
        {
            var items = GameContext.Instance.itemConfiguration.items;
            foreach (var pair in _objectivesModel.ItemIDToCountDic)
            {
                var id = pair.Key;
                _objectivesView.AddObjective(id, items[id].sprite, pair.Value);
            }
        }

        public void OnItemCollected(int itemID)
        {
            _objectivesModel.OnItemCollected(itemID);
        }
        
        public void Dispose()
        {
            _objectivesModel.OnObjectiveUpdateEvent -= OnObjectiveUpdate;
            _objectivesModel.Dispose();
        }

        public bool IsAllObjectivesComplete()
        {
            return _objectivesModel.IsAllObjectivesComplete();
        }
    }
}