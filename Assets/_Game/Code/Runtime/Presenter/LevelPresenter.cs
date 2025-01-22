using System;
using MatchHotel.Configuration;
using MatchHotel.Model;
using MatchHotel.View;
using UnityEngine.Assertions;

namespace MatchHotel.Presenter
{
    public class LevelPresenter
    {
        private readonly LevelView _levelView;
        private readonly InventoryModel _inventoryModel;
        private readonly InventoryPresenter _inventoryPresenter;
        private readonly ItemCollectPresenter _itemCollectPresenter;
        private readonly MergePresenter _mergePresenter;
        private ObjectivesPresenter _objectivesPresenter;
        private readonly TimerPresenter _timerPresenter;
        private readonly LevelData _levelData;
        
        private int time = 120;
        private bool _levelEnded;
        private readonly Action<bool> _onLevelEndAction;

        public LevelPresenter(LevelData levelData, LevelView levelView, Action<bool> onLevelEndAction)
        {
            _levelData = levelData;
            _levelView = levelView;
            _onLevelEndAction = onLevelEndAction;
            _inventoryModel = new InventoryModel();
            _inventoryPresenter = new InventoryPresenter(_inventoryModel, _levelView.InventoryView);
            _itemCollectPresenter = new ItemCollectPresenter(_levelView.ItemCollectView, _levelView.PileView, _inventoryPresenter);
            _mergePresenter = new MergePresenter(_inventoryModel);
            _timerPresenter = new TimerPresenter(_levelData.levelDuration, _levelView.TimerView, OnTimeExpired);
            var objectiveModel = PrepareLevel(_levelView.PileView);
            _objectivesPresenter = new ObjectivesPresenter(objectiveModel, _levelView.ObjectivesView);
        }
        
        private ObjectivesModel PrepareLevel(PileView pileView)
        {
            var objectiveModel = new ObjectivesModel();
            foreach (var itemAddressWithCount in _levelData.objectiveItems)
            {
                objectiveModel.AddObjective(itemAddressWithCount.itemAddress.itemID, itemAddressWithCount.count);
                AddToPile(pileView, itemAddressWithCount);
            }

            foreach (var itemAddressWithCount in _levelData.noiseItems)
            {
                AddToPile(pileView, itemAddressWithCount);
            }

            return objectiveModel;
        }

        private static void AddToPile(PileView pileView, ItemAddressWithCount itemAddressWithCount)
        {
            for (int i = 0; i < itemAddressWithCount.count; i++)
            {
                pileView.AddItem(ItemViewBuilder.Build(itemAddressWithCount.itemAddress.itemID));
            }
        }

        public void StartLevel()
        {
            _levelEnded = false;
            _inventoryModel.OnItemAddedEvent -= OnItemAdded;
            _inventoryModel.OnItemAddedEvent += OnItemAdded;
            _timerPresenter.StartTimer();
        }

        private void EndLevel(bool success)
        {
            Assert.IsFalse(_levelEnded);
            _onLevelEndAction?.Invoke(success);
            _levelEnded = true;
            if (success)
            {
                _levelView.OnLevelSucceed();
            }
            else
            {
                _levelView.OnLevelFailed();
            }
            Dispose();
        }
        
        private void OnTimeExpired()
        {
            EndLevel(false);
        }

        private void OnItemAdded(int itemID, int itemIndex)
        {
            _mergePresenter.OnItemAdded(itemID, itemIndex);
            _objectivesPresenter.OnItemCollected(itemID);
            if (_objectivesPresenter.IsAllObjectivesComplete())
            {
                EndLevel(true);
            }
            else if (_inventoryModel.IsFull())
            {
                EndLevel(false);
            }
        }

        public void Dispose()
        {
            _inventoryModel.OnItemAddedEvent -= OnItemAdded;
            _inventoryPresenter.Dispose();
            _itemCollectPresenter.Dispose();
            _mergePresenter.Dispose();
            _objectivesPresenter.Dispose();
            _timerPresenter.Dispose();
        }
    }
}