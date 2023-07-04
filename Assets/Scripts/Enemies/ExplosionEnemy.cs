using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class ExplosionEnemy : Enemy
    {
        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();

            if (_healthComponent != null)
                _healthComponent.OnDied += HandleDied;
        }

        private void HandleDied(HealthComponent healthComp)
        {
            healthComp.OnDied -= HandleDied;
            _enemyAttack.SpawnAttack();
            Destroy(gameObject);
        }

    }
}

