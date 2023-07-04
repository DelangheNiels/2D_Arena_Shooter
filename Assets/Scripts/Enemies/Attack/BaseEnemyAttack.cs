using Health;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Attack
{
    public class BaseEnemyAttack : MonoBehaviour
    {
        [SerializeField] protected float _lifeTime = 0.2f;
        [SerializeField] protected float _damage = 10.0f;

        protected float _timeAlive = 0.0f;

        public static Action<Enemy> OnAttackSpawned;

        // Update is called once per frame
        protected virtual void Update()
        {
            _timeAlive += Time.deltaTime;
            if (_timeAlive >= _lifeTime)
                Destroy(gameObject);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.tag != "Player")
                return;

            Damageable damageable = collision.gameObject.GetComponentInChildren<Damageable>();

            if (damageable == null)
                return;

            damageable.DealDamage(_damage);
        }
    }
}

