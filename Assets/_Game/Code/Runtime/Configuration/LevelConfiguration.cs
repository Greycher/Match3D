using System;
using System.Collections.Generic;
using System.Linq;
using MatchHotel.Common;
using MatchHotel.Model;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MatchHotel.Configuration
{
    [CreateAssetMenu(menuName = Constants.ProjectName + "/" + FileName, fileName = FileName)]
    public class LevelConfiguration : ScriptableObject
    {
        private const string FileName = nameof(LevelConfiguration);

        public LevelData[] levels;

        [Header("Level Generation Params")] 
        public int levelCountToGenerate = 50;
        public int minDifferentObjectiveItemCount = 3;
        public int maxDifferentObjectiveItemCount = 4;
        public int[] objectiveItemCountVariations = {6, 9, 12};
        [InfoBox("Non-objective item count will be determined " +
                 "by total objective item count multiplied with noise coefficient")]
        public float minNoiseCoefficient = 2.5f;
        public float maxNoiseCoefficient = 3.5f;
        public int[] levelDurationVariations = {60, 90, 120};
        
        [Button]
        public void GenerateLevels()
        {
            var start = levels.Length;
            var end = start + levelCountToGenerate;
            Array.Resize(ref levels, end);
            for (int i = start; i < end; i++)
            {
                levels[start + i] = GenerateLevelData();
            }
        }
        
        //Editor time function not written optimized
        private LevelData GenerateLevelData()
        {
            var itemConfig = GameContext.Instance.itemConfiguration;
            var pool = new List<ItemAddress>(itemConfig.objectiveItemPool);
            var keys = itemConfig.items.Keys;
            var noiseItemPool = new List<int>();
            foreach (var key in keys)
            {
                if (pool.Contains(new ItemAddress(key)))
                {
                    continue;
                }

                noiseItemPool.Add(key);
            }

            //Create Objectives Items
            var diffObjectiveItemCount =
                Random.Range(minDifferentObjectiveItemCount, maxDifferentObjectiveItemCount + 1);
            var objectiveItems = new ItemAddressWithCount[diffObjectiveItemCount];
            var totalObejctiveItemCount = 0;
            for (int j = 0; j < diffObjectiveItemCount; j++)
            {
                var rndIndex = Random.Range(0, pool.Count);
                var id = pool[rndIndex];
                var count = objectiveItemCountVariations[Random.Range(0, objectiveItemCountVariations.Length)];
                totalObejctiveItemCount += count;
                objectiveItems[j] = new ItemAddressWithCount(id, count);
                pool.RemoveAt(rndIndex);
            }

            var noise = Random.Range(minNoiseCoefficient, maxNoiseCoefficient);
            var noiseItemCount = Mathf.RoundToInt(totalObejctiveItemCount * noise);
            noiseItemCount -= noiseItemCount % 3;
            var rawNoiseItems = new int[noiseItemCount];
            //Create Noise Items
            for (int j = 0; j < noiseItemCount; j += 3)
            {
                var rndIndex = Random.Range(0, noiseItemPool.Count);
                rawNoiseItems[j] = noiseItemPool[rndIndex];
                rawNoiseItems[j + 1] = noiseItemPool[rndIndex];
                rawNoiseItems[j + 2] = noiseItemPool[rndIndex];
            }

            Dictionary<int, int> noiseItemsDic = new Dictionary<int, int>();
            foreach (int itemID in rawNoiseItems)
            {
                if (noiseItemsDic.ContainsKey(itemID))
                {
                    noiseItemsDic[itemID]++;
                }
                else
                {
                    noiseItemsDic[itemID] = 1;
                }
            }

            var noiseItems = noiseItemsDic.Select(pair => new ItemAddressWithCount(new ItemAddress(pair.Key), pair.Value))
                .ToArray();

            var levelDuration = levelDurationVariations[Random.Range(0, levelDurationVariations.Length)];
            return new LevelData(objectiveItems, noiseItems, levelDuration);
        }
    }
}