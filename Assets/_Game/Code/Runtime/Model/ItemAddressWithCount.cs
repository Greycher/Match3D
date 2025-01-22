using System;
using MatchHotel.Model;

namespace MatchHotel.Configuration
{
    [Serializable]
    public struct ItemAddressWithCount
    {
        public ItemAddress itemAddress;
        public int count;

        public ItemAddressWithCount(ItemAddress itemAddress, int count)
        {
            this.itemAddress = itemAddress;
            this.count = count;
        }
    }
}