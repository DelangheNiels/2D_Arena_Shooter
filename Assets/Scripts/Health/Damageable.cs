using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    [RequireComponent(typeof(HealthComponent))]
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private HealthComponent _healthComponent;

        public void DealDamage(float damage)
        {
            _healthComponent.TakeDamage(damage);
        }

      
    }
}

