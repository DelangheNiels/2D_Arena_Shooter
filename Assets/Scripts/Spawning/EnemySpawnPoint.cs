using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawning
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        public void Spawn(Enemy enemy)
        {
            Instantiate(enemy, transform);
        }
    }
}

