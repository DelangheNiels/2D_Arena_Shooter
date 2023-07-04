using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class SingleShotWeapon : BaseWeapon
    {
        private float _timeBetweenBullets;

        private bool _canFireBullet = true;
        private float _shootTimer;

        protected override void Start()
        {
            base.Start();
            _timeBetweenBullets = 1.0f / _weaponSettings.BulletsPerSecond;
        }

        protected override void Update()
        {
            base.Update();

            if (!_canFireBullet)
            {
                _shootTimer += Time.deltaTime;
                if(_shootTimer >= _timeBetweenBullets)
                {
                    _shootTimer = 0.0f;
                    _canFireBullet = true;
                }
            }
        }

        public override void Shoot()
        {
            if (!_canFireBullet)
                return;

            base.Shoot();

            SpawnBullet();
            _canFireBullet = false;
        }
    }
}

