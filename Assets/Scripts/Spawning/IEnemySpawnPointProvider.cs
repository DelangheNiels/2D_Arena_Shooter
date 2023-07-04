using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawning
{
    public interface IEnemySpawnPointProvider
    {
        List<EnemySpawnPoint> UsableSpawnPoints { get; }
        public void GenerateUsableSpawnPoints();
    }
}

