using System.Collections.Generic;
using MatchHotel.Model;
using MatchHotel.View;
using UnityEngine.Assertions;

namespace MatchHotel.Presenter
{
    public class InventoryPresenter
    {
        private readonly InventoryView _inventoryView;
        private readonly InventoryModel _inventoryModel;

        public InventoryPresenter(InventoryModel inventoryModel, InventoryView view)
        {
            _inventoryModel = inventoryModel;
            _inventoryView = view;
            _inventoryModel.OnItemsMergedEvent += OnItemsMerged;
        }
        
        private void OnItemsMerged(int middleItemIndex)
        {
            _inventoryView.MergeItems(middleItemIndex);
        }

        public void AddItem(ItemView itemView)
        {
            Assert.IsFalse(_inventoryModel.IsFull());
            var itemIndex = _inventoryModel.LookAHeadIndex(itemView.ItemID, out var node);
            _inventoryView.AddItemToSlot(itemView, itemIndex);
            _inventoryModel.AddItem(itemView.ItemID);
        }

        public void Dispose()
        {
            _inventoryModel.OnItemsMergedEvent -= OnItemsMerged;
        }
    }
}