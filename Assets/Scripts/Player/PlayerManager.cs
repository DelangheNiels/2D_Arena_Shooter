using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private MovementComponent _playerMovement;
        private WeaponLoadout _weaponLoadout;
        private HealthComponent _healthComponent;

        private bool _isLeftMousePressed;
        // Start is called before the first frame update
        void Start()
        {
            _playerMovement = GetComponentInChildren<MovementComponent>();
            _weaponLoadout = GetComponentInChildren<WeaponLoadout>();

            _healthComponent = GetComponentInChildren<HealthComponent>();
            _healthComponent.OnDied += HandlePlayerDied;

        }

        private void Update()
        {
            HandleMousePressed();
        }

        private void OnMove(InputValue inputValue)
        {
            if (_playerMovement == null)
                return;

            _playerMovement.Move(inputValue.Get<Vector2>());
        }

        private void OnReload()
        {
            if (_weaponLoadout.ActiveWeapon == null)
                return;

            _weaponLoadout.ActiveWeapon.StartReloading();
        }

        private void OnSwitchWeapon(InputValue inputValue)
        {
           _weaponLoadout.SwitchActiveWeapon((int)inputValue.Get<Vector2>().y);
        }

        private void StartShooting()
        {
            if (_weaponLoadout.ActiveWeapon == null)
                return;

            _weaponLoadout.ActiveWeapon.Shoot();
        }

        private void StopShooting()
        {
            if (_weaponLoadout.ActiveWeapon == null)
                return;

            _weaponLoadout.ActiveWeapon.StopShooting();
        }

        private void HandleMousePressed()
        {
            if (Mouse.current.leftButton.isPressed && _isLeftMousePressed == false)
            {
                _isLeftMousePressed = true;
                StartShooting();
            }

            if(!Mouse.current.leftButton.isPressed && _isLeftMousePressed)
            {
                _isLeftMousePressed = false;
                StopShooting();
            }
        }

        private void HandlePlayerDied(HealthComponent healthComp)
        {
            healthComp.OnDied -= HandlePlayerDied;
            Debug.Log("The player has died");
        }
    }
}

