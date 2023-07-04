using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Attack
{
    public class EnemyExplosionAttack : BaseEnemyAttack
    {
        private void Awake()
        {
            transform.SetParent(null);
            OnAttackSpawned += HandleAttackSpawned;
        }
     
        private void HandleAttackSpawned(Enemy enemy)
        {
            OnAttackSpawned -= HandleAttackSpawned;
            Destroy(enemy.transform.root.gameObject);
        }
    }
}

