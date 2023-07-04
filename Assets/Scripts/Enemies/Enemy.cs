using Abilities;
using Enemies.Attack;
using Health;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected EnemySpawnSettings _enemySpawnSettings;

        protected EnemyAttackManager _enemyAttack;
        protected HealthComponent _healthComponent;
        protected MovementComponent _movementComponent;
        protected BaseAbility _ability;

        public EnemyAttackManager EnemyAttack => _enemyAttack;
        public HealthComponent HealthComponent => _healthComponent;
        public MovementComponent MovementComponent => _movementComponent;
        public EnemySpawnSettings EnemySpawnSettings => _enemySpawnSettings;
        public BaseAbility Ability => _ability;

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            _enemyAttack = GetComponentInChildren<EnemyAttackManager>();
            _healthComponent = GetComponentInChildren<HealthComponent>();
            _movementComponent = GetComponentInChildren<MovementComponent>();
            _ability = GetComponentInChildren<BaseAbility>();

        }
    }
}

