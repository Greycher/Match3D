using System;

namespace MatchHotel.Model
{
    [Serializable]
    //This class ads no implementation and only used for ItemAddressDrawer
    //This way it support single or array properties out of the box
    public struct ItemAddress
    {
        public int itemID;

        public ItemAddress(int itemID)
        {
            this.itemID = itemID;
        }
    }
}