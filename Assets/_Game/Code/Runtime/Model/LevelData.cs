using System;

namespace MatchHotel.Configuration
{
    [Serializable]
    public struct LevelData
    {
        public ItemAddressWithCount[] objectiveItems;
        public ItemAddressWithCount[] noiseItems;
        public int levelDuration;

        public LevelData(ItemAddressWithCount[] objectiveItems, ItemAddressWithCount[] noiseItems, int levelDuration)
        {
            this.objectiveItems = objectiveItems;
            this.noiseItems = noiseItems;
            this.levelDuration = levelDuration;
        }
    }
}