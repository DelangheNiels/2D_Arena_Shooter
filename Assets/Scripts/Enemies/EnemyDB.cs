using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu]
    public class EnemyDB : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
        private static List<Enemy> _staticEnemyList = new List<Enemy>();

        private void Awake()
        {
            _staticEnemyList = _enemies;
        }

        public static List<Enemy> GetEnemies()
        {
            return _staticEnemyList;
        }

        public static EnemySpawnSettings GetSpawnSettings(Enemy enemy)
        {
            if(enemy == null || !_staticEnemyList.Contains(enemy))
                return null;

            var foundEnemy = _staticEnemyList.Find(e => e == enemy);
            return foundEnemy.EnemySpawnSettings;
        }

        public static Enemy GetRandomEnemy()
        {
            int randomNumber = Random.Range(0, _staticEnemyList.Count);
            return _staticEnemyList[randomNumber];
        }
       
    }
}

