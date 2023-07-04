using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _maxHealth = 100.0f;
        [SerializeField] private DamageNumber _damageNumberPrefab;

        private float _currentHealth;

        private Animator _animator;

        public Action<HealthComponent> OnDied;
        public Action OnTookDamage;
        public Action OnHealthChanged;

        public float CurrentHealth => _currentHealth;
        public bool IsDead => _currentHealth <= 0;

        // Start is called before the first frame update
        void Start()
        {
            _currentHealth = _maxHealth;
            _animator = transform.root.GetComponentInChildren<Animator>();
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            OnTookDamage?.Invoke();
            OnHealthChanged?.Invoke();

            if (_currentHealth <= 0)
            {
                OnDied?.Invoke(this);

                if(_animator != null)
                    _animator.SetBool("IsDead", true);
            }
            else
            {
                if (_damageNumberPrefab != null)
                {
                    var damageNumber = Instantiate(_damageNumberPrefab, transform.position, Quaternion.identity);
                    damageNumber.SetDamage((int)damage);
                }
            }
        }

        public float GetHealthPercentage()
        {
            return _currentHealth / _maxHealth;
        }

        public void AddHealth(float health)
        {
            if (_currentHealth >= _maxHealth)
                return;

            OnHealthChanged?.Invoke();

            if(_currentHealth + health >= _maxHealth)
            {
                _currentHealth = _maxHealth;
                return;
            }

            _currentHealth += health;
        }
    }
}

