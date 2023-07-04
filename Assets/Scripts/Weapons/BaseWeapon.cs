using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Weapons
{
    [RequireComponent(typeof(WeaponRotation))]
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletSpawnLocation;

        [SerializeField]protected BaseWeaponSettings _weaponSettings;
        [SerializeField] private Sprite _weaponImage;

        protected int _currentAmountInMagazine;

        private bool _isReloading;
        private float _reloadTimer;

        public Action OnAmmoAmountChanged;

        public Sprite WeaponImage => _weaponImage;
        public int MaxMagazineSize => _weaponSettings.MagazineSize;
        public int CurrentAmountInMagazine => _currentAmountInMagazine;

        protected virtual void Start()
        {
            _currentAmountInMagazine = _weaponSettings.MagazineSize;
        }

        protected virtual void Update()
        {
            if(_isReloading)
            {
                _reloadTimer += Time.deltaTime;
                if(_reloadTimer >= _weaponSettings.ReloadTime)
                {
                    _currentAmountInMagazine = _weaponSettings.MagazineSize;
                    OnAmmoAmountChanged?.Invoke();
                    _isReloading = false;
                    _reloadTimer = 0;
                }
            }
        }

        protected void SpawnBullet()
        {
            if (_currentAmountInMagazine <= 0)
                return;

            var projectile = Instantiate(_weaponSettings.ProjectilePrefab, _bulletSpawnLocation.transform.position,_bulletSpawnLocation.transform.rotation);
            Vector2 moveVector = Mouse.current.position.ReadValue() - (Vector2)Camera.main.WorldToScreenPoint(_bulletSpawnLocation.transform.position);
            projectile.SetMovementVector(moveVector.normalized);

            --_currentAmountInMagazine;
            OnAmmoAmountChanged?.Invoke();
        }

        public virtual void Shoot()
        {
            if (_currentAmountInMagazine >= 0)
                return;

            // Stop reload when player wants to shoot while reloading
            if (_isReloading)
            {
                _isReloading = false;
                _reloadTimer = 0.0f;
            }
        }

        public virtual void StopShooting()
        {

        }

        public void StartReloading()
        {
            if (_currentAmountInMagazine == _weaponSettings.MagazineSize)
                return;

            _isReloading = true;
        }

    }
}

