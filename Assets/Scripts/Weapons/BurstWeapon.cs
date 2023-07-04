using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class BurstWeapon : BaseWeapon
    {
        [SerializeField] private float _burstSize;
        [SerializeField] private float _burtsTime;

        private float _timeBetweenBullets;
        private bool _canFireBullet = true;
        private float _shootTimer;

        private float _timeBetweenBurstBullet;
        private float _burstTimer;
        private int _nrOfBurstBulletsShot;

        protected override void Start()
        {
            base.Start();

            _timeBetweenBullets = 1.0f / _weaponSettings.BulletsPerSecond;

            if(_burtsTime > _timeBetweenBullets)
                _burtsTime = _timeBetweenBullets;

            _timeBetweenBurstBullet = _burtsTime / _burstSize;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if(!_canFireBullet)
            {
                _shootTimer += Time.deltaTime;
                if (_shootTimer >= _timeBetweenBullets)
                {
                    _shootTimer = 0.0f;
                    _canFireBullet = true;
                    _nrOfBurstBulletsShot = 0;
                    _burstTimer = 0.0f;
                }

                ShootBurst();
            }
        }

        public override void Shoot()
        {
            if (!_canFireBullet)
                return;

            base.Shoot();

            _canFireBullet = false;

        }

        private void ShootBurst()
        {
            _burstTimer += Time.deltaTime;
            if(_burstTimer >= _timeBetweenBurstBullet && _nrOfBurstBulletsShot < _burstSize)
            {
                _burstTimer = 0.0f;
                SpawnBullet();
                ++_nrOfBurstBulletsShot;
            }
        }
    }
}

