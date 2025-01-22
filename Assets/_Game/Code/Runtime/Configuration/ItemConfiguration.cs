using System.Collections.Generic;
using MatchHotel.Common;
using MatchHotel.Model;
using MatchHotel.View;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MatchHotel.Configuration
{
    [CreateAssetMenu(menuName = Constants.ProjectName + "/" + FileName, fileName = FileName)]
    public class ItemConfiguration : SerializedScriptableObject
    {
        private const string FileName = nameof(ItemConfiguration);
        public Dictionary<int, ItemData> items = new();
        public ItemAddress[] objectiveItemPool;
    }
}