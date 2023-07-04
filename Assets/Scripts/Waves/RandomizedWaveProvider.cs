using Enemies;
using Spawning;
using System.Collections.Generic;
using UnityEngine;

namespace Waves
{
    public class RandomizedWaveProvider : WaveProvider
    {
        protected override void GenerateWaveItems()
        {
            var waveData = _currentwave.EnemyWaveData;

            //divide wave data over spawn points
            var tempWaveItems = CreateWaveItemListFromWaveData(waveData);

            _waveItems = RandomizeWaveItems(tempWaveItems);
           
        }

        private List<WaveItem> CreateWaveItemListFromWaveData(Dictionary<Enemy, int> enemyWavedata)
        {
            List<WaveItem> waveItems = new List<WaveItem>();

            foreach (var waveData in enemyWavedata)
            {
                int amountToDistrubute = waveData.Value;
                while(amountToDistrubute > 0)
                {
                    //get amount to put in cluster
                    int amount = GetRandomEnemyAmount(amountToDistrubute);
                    amountToDistrubute -= amount;

                    //get random spawnpoint
                    EnemySpawnPoint spawnPoint = GetRandomSpawnPoint();

                    waveItems.Add(new WaveItem(waveData.Key, amount, spawnPoint));
                }
            }

            return waveItems;
        }

        private int GetRandomEnemyAmount(int amountToDistrubute)
        {
            int amount = 0;
            if (amountToDistrubute <= _minimumEnemyClusterSize)
                amount = amountToDistrubute;
            else
            {
                int maxClusterSize = _maximumEnemyClusterSize;
                if (amountToDistrubute < _maximumEnemyClusterSize)
                    maxClusterSize = amountToDistrubute;

                amount = Random.Range(_minimumEnemyClusterSize, maxClusterSize);
            }

            return amount;
        }

        private EnemySpawnPoint GetRandomSpawnPoint()
        {
            int randomSpawnPointIndex = Random.Range(0, _spawnPointProvider.UsableSpawnPoints.Count);
            EnemySpawnPoint enemySpawnPoint = _spawnPointProvider.UsableSpawnPoints[randomSpawnPointIndex];

            if (enemySpawnPoint == null)
            {
                Debug.LogError("Could not find enemy spawn point");
            }

            return enemySpawnPoint;
        }

        private List<WaveItem> RandomizeWaveItems(List<WaveItem> waveItems)
        {
            List<WaveItem> randomizedList = new List<WaveItem>();

            List<int> waveItemIndices = new List<int>();
            for(int i=0; i < waveItems.Count; i++)
                waveItemIndices.Add(i);

            int amount = 0;
            while(amount < waveItems.Count)
            {
                ++amount;
                int randomIndex = Random.Range(0, waveItemIndices.Count);
                WaveItem randomWaveItem = waveItems[waveItemIndices[randomIndex]];
                randomizedList.Add(randomWaveItem);
                waveItemIndices.RemoveAt(randomIndex);
            }

            return randomizedList;
        }
    }
}

