using MatchHotel.Configuration;
using UnityEngine;

namespace MatchHotel.View
{
    public class ItemViewBuilder
    {
        public static ItemView Build(int id)
        {
            var itemData = GameContext.Instance.itemConfiguration.items[id];
            var view = Object.Instantiate(itemData.itemView);
            view.InjectItemID(id);
            return view;
        }
    }
}