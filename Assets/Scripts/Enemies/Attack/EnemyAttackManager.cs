using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Attack
{
    public class EnemyAttackManager : MonoBehaviour
    {
        [SerializeField] private EnemyAttackSettings _enemyAttackSettings;
        [SerializeField] private Transform _attackSpawnLocation;
        [SerializeField] private BaseEnemyAttack _attackPrefab;

        private float _attackTimer;
        private bool _canAttack;

        private Animator _animator;

        public bool CanAttack => _canAttack;
        public EnemyAttackSettings Settings => _enemyAttackSettings;

        protected virtual void Start()
        {
            _canAttack = true;
            _attackTimer = _enemyAttackSettings.AttackSpeed;
            _animator = transform.root.GetComponentInChildren<Animator>();
        }

        protected virtual void Update()
        {
            if (_canAttack)
                return;

            _attackTimer += Time.deltaTime;
            if (_attackTimer > _enemyAttackSettings.AttackSpeed)
            {
                _canAttack = true;
                _attackTimer = 0.0f;
            }
        }

        //Start attack animation
        public void Attack()
        {
            if (!_canAttack)
                return;

            if (_animator != null)
                _animator.SetBool("IsAttacking", true);

            _canAttack = false;

        }

        //Do attack that gets called in an animation event
        public void SpawnAttack()
        {
            var attack = Instantiate(_attackPrefab, _attackSpawnLocation);
            _animator.SetBool("IsAttacking", false);

            var enemy = transform.root.GetComponent<Enemy>();
            BaseEnemyAttack.OnAttackSpawned?.Invoke(enemy);

            //check if it is a projectile attack
            EnemyProjectileAttack enemyProjectileAttack = attack as EnemyProjectileAttack;
            if(enemyProjectileAttack != null)
            {
                Vector2 moveVector = FindObjectOfType<PlayerManager>().transform.position - transform.position;
                enemyProjectileAttack.SetMovementVector(moveVector);
            }
        }
    }
}

