using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Waves
{
    public class Wave
    {
        private int _waveIndex;
        private int _waveStrength;
        private WaveDifficultySettings _difficultySettings;

        private Dictionary<Enemy, int> _enemyWaveData = new Dictionary<Enemy, int>();

        private int _totalAmountOfEnemies;
        private int _amountOfEnemiesDied = 0;

        public int Index => _waveIndex;
        public int TotalAmountOfEnemies => _totalAmountOfEnemies;
        public int AmountOfEnemiesDied => _amountOfEnemiesDied;

        public Dictionary<Enemy, int> EnemyWaveData => _enemyWaveData; 

        public Wave(int index, WaveDifficultySettings difficultySettings)
        {
            _waveIndex = index;
            _waveStrength = Mathf.RoundToInt(difficultySettings.WaveDifficultyCurve.Evaluate(_waveIndex));
            _difficultySettings = difficultySettings;

            GenerateEnemyWaveData();
        }

        //Generate the amount of enemies that will spawn for each type this wave
        private void GenerateEnemyWaveData()
        {
            int waveStrenghtLeft = _waveStrength;

            while(waveStrenghtLeft > 0)
            {
                Enemy enemy = GetEnemy(waveStrenghtLeft);

                if (enemy == null)
                {
                    Debug.LogError("Eenmy not found");
                    break;
                }
                else
                {
                    ++_totalAmountOfEnemies;

                    waveStrenghtLeft -= EnemyDB.GetSpawnSettings(enemy).Strength;

                    if (_enemyWaveData.ContainsKey(enemy))
                        ++_enemyWaveData[enemy];
                    else
                        _enemyWaveData.Add(enemy, 1);
                }
              
            }
        }

        private Enemy GetEnemy(int waveStrenght)
        {
            List<Enemy> spawnableEnemies = GetSpawnableEnemies(waveStrenght); 
            return GetRandomWeightedEnemy(spawnableEnemies);
        }

        private List<Enemy> GetSpawnableEnemies(int waveStrenght)
        {
            if(waveStrenght <= 0)
                return null;

            List<Enemy> spawnableEnemies = new List<Enemy>();
            List<Enemy> enemies = GetAllEnemiesThatCanSpawnThisWave();

            foreach(Enemy enemy in enemies)
            {
                EnemySpawnSettings settings = EnemyDB.GetSpawnSettings(enemy);
                if(settings.Strength <= waveStrenght)
                    spawnableEnemies.Add(enemy);
            }

            return spawnableEnemies;
        }

        private List<Enemy> GetAllEnemiesThatCanSpawnThisWave()
        {
            List<Enemy> enemies = new List<Enemy>();

            var spawnList = _difficultySettings.EnemySpawnList;
            foreach(var enemySpawn in spawnList)
            {
                if (enemySpawn.WaveIndex > _waveIndex)
                    continue;

                enemies.Add(enemySpawn.Enemy);
            }

            return enemies;
        }

        //Get a random enemy by using their weights
        private Enemy GetRandomWeightedEnemy(List<Enemy> enemies)
        {
            Enemy randomEnemy = null;

            int totalWeight = GetTotalWeight(enemies);
            int prevWeight = 0;

            int randomWeight = Random.Range(0, totalWeight);

            foreach(Enemy enemy in enemies)
            {
                int enemyWeight = EnemyDB.GetSpawnSettings(enemy).Weight;

                if(randomWeight >= prevWeight && randomWeight <= enemyWeight + prevWeight)
                {
                    randomEnemy = enemy;
                    break;
                }

                prevWeight += enemyWeight;
            }

            if(randomEnemy == null)
            {
                Debug.LogError("random enemy is null, the random weight was: "+ randomWeight);
            }

            return randomEnemy;
        }

        private int GetTotalWeight(List<Enemy> enemies)
        {
            int total = 0;

            foreach(Enemy enemy in enemies)
            {
                EnemySpawnSettings spawnSettings = EnemyDB.GetSpawnSettings(enemy);
                total += spawnSettings.Weight;
            }

            return total;
        }

        public void IncreaseDeadEnemies()
        {
            ++_amountOfEnemiesDied;
        }

       
    }
}

