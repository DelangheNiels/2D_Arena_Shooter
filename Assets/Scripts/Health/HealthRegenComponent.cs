using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public class HealthRegenComponent : MonoBehaviour
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private float _healthPerSecond = 5.0f;
        [SerializeField] private float _timeToStartRegen = 5.0f;

        private float _timer = 0;
        private bool _canRegen = true;

        private void Start()
        {
            _healthComponent.OnTookDamage += HandleTookDamage;
        }

        void Update()
        {
            if (_healthComponent.IsDead)
                return;

            if(_canRegen)
            {
                float health = _healthPerSecond * Time.deltaTime;
                _healthComponent.AddHealth(health);
            }
            else
            {
                _timer += Time.deltaTime;
                if(_timer > _timeToStartRegen)
                {
                    _canRegen = true;
                    _timer = 0;
                }
            }
        }

        private void OnDestroy()
        {
            _healthComponent.OnTookDamage -= HandleTookDamage;
        }

        private void HandleTookDamage()
        {
            _canRegen = false;
            _timer = 0.0f;
        }
    }
}

