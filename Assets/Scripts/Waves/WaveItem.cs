using Enemies;
using Spawning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Waves
{
    public struct WaveItem
    {
        public Enemy Enemy;
        public int Amount;
        public EnemySpawnPoint SpawnPoint;

        public WaveItem(Enemy enemy, int amount, EnemySpawnPoint spawnPoint)
        {
            Enemy = enemy;
            Amount = amount;
            SpawnPoint = spawnPoint;
        }
    }
}

