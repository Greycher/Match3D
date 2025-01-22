using System.Collections.Generic;
using MatchHotel.Common;
using MatchHotel.Model;

namespace MatchHotel.Presenter
{
    public class MergePresenter
    {
        private Dictionary<int, int> _countPerItemDic = new();
        private readonly InventoryModel _inventoryModel;

        public MergePresenter(InventoryModel inventoryModel)
        {
            _inventoryModel = inventoryModel;
        }

        public void OnItemAdded(int itemID, int itemIndex)
        {
            if (!_countPerItemDic.TryGetValue(itemID, out var count))
            {
                _countPerItemDic.Add(itemID, 0);
            }

            if (++count == Constants.CountForMerge)
            {
                count = 0;
                _inventoryModel.MergeItems(itemID);
            }
            
            _countPerItemDic[itemID] = count;
        }
        
        public void Dispose() {}
    }
}