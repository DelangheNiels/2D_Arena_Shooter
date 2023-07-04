using BehaviourTree;
using Enemies;
using Health;
using Spawning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Waves;

namespace States.WaveManager
{
    public class WaveManagerSpawningState : WaveManagerState
    {
        private int _amountOfEnemiesSpawned = 0;
        private int _amountOfEnemiesCurrentlyAllive = 0;

        private WaveProvider _waveProvider;

        private Dictionary<Enemy, int> _spawnedEnemys = new Dictionary<Enemy, int>();
        private List<WaveItem> _waveItemsToSpawn = new List<WaveItem>();

        private bool _canSpawn = false;

        public WaveManagerSpawningState(Blackboard blackboard, WaveProvider waveProvider) : base(blackboard)
        {
            _waveProvider = waveProvider;
        }

        public override void OnEnter()
        {
            _amountOfEnemiesSpawned = 0;
            _spawnedEnemys.Clear();

            _waveItemsToSpawn.Clear();
            _waveItemsToSpawn = _waveProvider.WaveItems;

            _canSpawn = false;

            _waveProvider.StartCoroutine(CoSpawnWave());
        }

        public override void OnExit()
        {
            _blackboard.SetData("HaveAllEnemiesSpawned", true);
        }

        private IEnumerator CoSpawnWave()
        {
          
            if(_amountOfEnemiesSpawned < _waveProvider.CurrentWave.TotalAmountOfEnemies)
            {
                yield return CoSpawnEnemies();
            }

            _canSpawn = true;
            yield return null;
        }

        private IEnumerator CoSpawnEnemies()
        {
            //get amount of enemies that I can spawn
            int amountToSpawn = GetAmountOfEnemiesToSpawn();
            _amountOfEnemiesSpawned += amountToSpawn;

            // get wave items for these enemies
            List<WaveItem> itemsToSpawn = GetWaveItemsToSpawn(amountToSpawn);

            //Spawn all enemies
            foreach(WaveItem item in itemsToSpawn)
            {
                for(int i = 0; i < item.Amount; i++)
                {
                    SpawnEnemy(item.Enemy, item.SpawnPoint);
                    yield return new WaitForSeconds(0.5f);
                }

                yield return new WaitForSeconds(3.0f);

            }

            yield return null;
        }

        private IEnumerator CoSpawnEnemy(Enemy enemy, EnemySpawnPoint spawnPoint)
        {
            var spawnedEnemy = WaveProvider.Instantiate(enemy, spawnPoint.transform);
            spawnedEnemy.transform.parent = null;
            spawnedEnemy.HealthComponent.OnDied += HandleEnemyDied;
            ++_amountOfEnemiesCurrentlyAllive;
            yield return null;
        }

        private void SpawnEnemy(Enemy enemy, EnemySpawnPoint spawnPoint)
        {
            _waveProvider.StartCoroutine(CoSpawnEnemy(enemy, spawnPoint));
        }

        private int GetAmountOfEnemiesToSpawn()
        {
            int possibleAmount = _waveProvider.Settings.AmountOfEnemiesAlliveAtOnce - _amountOfEnemiesCurrentlyAllive;

            possibleAmount = Mathf.Clamp(possibleAmount, 0, _waveProvider.CurrentWave.TotalAmountOfEnemies - _amountOfEnemiesSpawned);

            return possibleAmount;
        }

        private List<WaveItem> GetWaveItemsToSpawn(int amountOfEnemies)
        {
            List<WaveItem> itemsToSpawn = new List<WaveItem>();
            List<WaveItem> itemsToRemove = new List<WaveItem>();

           for(int i=0; i <  _waveItemsToSpawn.Count; i++)
            {
                if (amountOfEnemies <= 0)
                    break;

                if(_waveItemsToSpawn[i].Amount < amountOfEnemies)
                {
                    itemsToSpawn.Add(_waveItemsToSpawn[i]);
                    amountOfEnemies -= _waveItemsToSpawn[i].Amount;
                    itemsToRemove.Add(_waveItemsToSpawn[i]);
                    continue;

                }
                else
                {
                    _waveItemsToSpawn[i] = new WaveItem()
                    {
                        Amount =_waveItemsToSpawn[i].Amount - amountOfEnemies,
                        Enemy = _waveItemsToSpawn[i].Enemy,
                        SpawnPoint = _waveItemsToSpawn[i].SpawnPoint
                    };

                    WaveItem waveItemToAdd = new WaveItem()
                    {
                        Amount = amountOfEnemies,
                        Enemy = _waveItemsToSpawn[i].Enemy,
                        SpawnPoint = _waveItemsToSpawn[i].SpawnPoint
                    };

                    itemsToSpawn.Add(waveItemToAdd);
                    amountOfEnemies = 0;
                    break;
                }
            }

            //Remove all wave items that will completely spawn
            foreach(WaveItem item in itemsToRemove)
            {
                _waveItemsToSpawn.Remove(item);
            }

            return itemsToSpawn;
        }

        private void HandleEnemyDied(HealthComponent healthComp)
        {
            _waveProvider.CurrentWave.IncreaseDeadEnemies();
            healthComp.OnDied -= HandleEnemyDied;
            --_amountOfEnemiesCurrentlyAllive;
        }

        public override void Update()
        {
            if (!_canSpawn)
                return;

            if(_amountOfEnemiesSpawned == _waveProvider.CurrentWave.TotalAmountOfEnemies)
            {
                _blackboard.SetData("HaveAllEnemiesSpawned", true);
                return;
            }

            if(_amountOfEnemiesSpawned < _waveProvider.CurrentWave.TotalAmountOfEnemies
                && _amountOfEnemiesCurrentlyAllive <= _waveProvider.Settings.EnemiesAlliveToSpawnMore)
            {
                _waveProvider.StartCoroutine(CoSpawnEnemies());
            }
        }

    }
}

