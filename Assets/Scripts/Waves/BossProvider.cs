using Enemies;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Waves
{
    public class BossProvider : MonoBehaviour
    {
        [SerializeField] BossEnemy _bossPrefab;
        [SerializeField] Transform _bossSpawnLocation;

        public void SpawnBoss()
        {
            var boss = Instantiate(_bossPrefab, _bossSpawnLocation);
            boss.transform.parent = null;
            NotificationManager.CreateNotification("Boss has spawned!", 3.0f);
        }

    }
}

