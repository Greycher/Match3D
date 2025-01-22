using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace MatchHotel.Model
{
    public class ObjectivesModel
    {
        private Dictionary<int, int> _itemIDToCountDic = new();

        public Dictionary<int, int> ItemIDToCountDic => _itemIDToCountDic;

        public delegate void OnObjectiveUpdateAction(int itemID, int remainingCount);
        public OnObjectiveUpdateAction OnObjectiveUpdateEvent;
        
        public void AddObjective(int itemID, int count)
        {
            Assert.IsFalse(_itemIDToCountDic.ContainsKey(itemID));
            Debug.Log($"New objective added, id:{itemID}, count: {count}");
            _itemIDToCountDic.Add(itemID, count);
        }

        public void OnItemCollected(int itemID)
        {
            if (_itemIDToCountDic.TryGetValue(itemID, out var count))
            {
                _itemIDToCountDic[itemID] = --count;
                OnObjectiveUpdateEvent?.Invoke(itemID, count);
                Debug.Log($"Objective item collected, id: {itemID}, remaining: {count}");
                if (count == 0)
                {
                    Debug.Log($"Objective completed, id: {itemID}");
                }
            }
        }

        public bool IsAllObjectivesComplete()
        {
            foreach (var count in _itemIDToCountDic.Values)
            {
                if (count > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void Dispose()
        {
            OnObjectiveUpdateEvent = null;
        }
    }
}