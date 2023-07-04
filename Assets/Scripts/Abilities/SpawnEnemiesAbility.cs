using Enemies;
using Spawning;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Abilities
{
    public class SpawnEnemiesAbility : BaseAbility
    {
        [SerializeField] int _amountOfEnemies = 5;

        private List<EnemySpawnPoint> _spawnPoints;

        protected override void Awake()
        {
            base.Awake();
            _spawnPoints = FindObjectsOfType<EnemySpawnPoint>().ToList();

        }
        public override void DoAbility()
        {
            if (_animator != null)
                _animator.SetBool("UseAbility", false);

            for (int i=0; i < _amountOfEnemies; i++)
            {
                var enemy = EnemyDB.GetRandomEnemy();
                var randomNumber = Random.Range(0,_spawnPoints.Count);
                var spawnPoint = _spawnPoints[randomNumber];
                SpawnEnemy(spawnPoint, enemy);
            }
        }

        private void SpawnEnemy(EnemySpawnPoint spawnPoint, Enemy enemy)
        {
            var spawnedEnemy = Instantiate(enemy, spawnPoint.transform);
            spawnedEnemy.transform.parent = null;
        }
    }
}

