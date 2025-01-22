using System;
using UnityEngine;

namespace MatchHotel.Model.Runtime.Model
{
    public class GameModel
    {
        private const string LevelIndexPrefKey = "level_index";

        public Action<int> OnLevelIndexChangeEvent;
        
        public int GetCurrentLevelIndex()
        {
            return PlayerPrefs.GetInt(LevelIndexPrefKey, 0);
        }
        
        public void SetCurrentLevelIndex(int levelIndex)
        {
            PlayerPrefs.SetInt(LevelIndexPrefKey, levelIndex);
            OnLevelIndexChangeEvent?.Invoke(levelIndex);
        }
    }
}