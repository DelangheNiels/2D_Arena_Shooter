using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class AutomaticWeapon : BaseWeapon
    {
        private float _timeBetweenBullets;
        private float _shootTimer;
        private bool _isShooting;
        
        protected override void Start()
        {
            base.Start();

            _timeBetweenBullets = 1.0f / _weaponSettings.BulletsPerSecond;
            _shootTimer = _timeBetweenBullets;
        }

        protected override void Update()
        {
            base.Update();

            if (!_isShooting)
                return;

            _shootTimer += Time.deltaTime;
            if(_shootTimer >= _timeBetweenBullets)
            {
                SpawnBullet();
                _shootTimer = 0;
            }

        }

        public override void Shoot()
        {
            base.Shoot();

            _isShooting = true;
            _shootTimer = _timeBetweenBullets;
        }

        public override void StopShooting()
        {
            _isShooting = false;
        }
    }
}

