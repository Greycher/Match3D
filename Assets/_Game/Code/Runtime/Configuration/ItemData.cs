using System;
using MatchHotel.View;
using UnityEngine;

namespace MatchHotel.Configuration
{
    [Serializable]
    public class ItemData
    {
        public String itemName;
        public ItemView itemView;
        public Sprite sprite;
    }
}