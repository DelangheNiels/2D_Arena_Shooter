using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Spawning
{
    public class RandomSpawnPointProvider : MonoBehaviour, IEnemySpawnPointProvider
{
        private List<EnemySpawnPoint> _usableSpawnPoints = new List<EnemySpawnPoint>();

        public List<EnemySpawnPoint> UsableSpawnPoints => _usableSpawnPoints;

        public void GenerateUsableSpawnPoints()
        {
            if (_usableSpawnPoints.Count > 0)
                return;

            _usableSpawnPoints = FindObjectsOfType<EnemySpawnPoint>().ToList();
        }

      
    }
}

