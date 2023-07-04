using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Player
{
    public class WeaponLoadout : MonoBehaviour
    {
        private const int _amountOfWeapons = 3;
        [SerializeField] private GameObject _weaponHoldLocation;
        [SerializeField] private BaseWeapon[] _weapons = new BaseWeapon[_amountOfWeapons];
        private BaseWeapon _activeWeapon;
        private int _activeWeaponIndex = -1;

        //index of previous weapon and new active weapon
        public Action<int, int> OnActiveWeaponChanged;

        public BaseWeapon ActiveWeapon => _activeWeapon;
        public BaseWeapon[] Weapons => _weapons;

        void Start()
        {
            //Set actve weapon to first weapon in weapons array
            SetActiveWeapon();
        }

        private void SetActiveWeapon()
        {
            for (int i = 0; i < _amountOfWeapons; i++)
            {
                if (_weapons[i] == null)
                    continue;

                _activeWeapon = _weapons[i];
                _activeWeaponIndex = i;
                _weapons[i].gameObject.SetActive(true);
                _activeWeapon.transform.position = _weaponHoldLocation.transform.position;
                break;
            }
        }

        public void SwitchActiveWeapon(int scrollValue)
        {
            //go to previous weapon in list
            if(scrollValue > 0)
            {
                int newWeaponIndex = GetIndexOfPrevWeapon();

                if (newWeaponIndex == _activeWeaponIndex)
                    return;

                SetNewActiveWeapon(newWeaponIndex);
            }

            //go to next weapon in list
            if(scrollValue < 0)
            {
                int newWeaponIndex = GetIndexOfNextWeapon();

                if (newWeaponIndex == _activeWeaponIndex)
                    return;

                SetNewActiveWeapon(newWeaponIndex);
            }
        }

        private void SetNewActiveWeapon(int index)
        {
            if(_activeWeapon != null)
            {
                _activeWeapon.gameObject.SetActive(false);
            }

            BaseWeapon prevWeapon = _activeWeapon;
            int prevWeaponIndex = GetIndexOfWeapon(prevWeapon);

            _activeWeapon = _weapons[index];
            _activeWeaponIndex = index;
            _weapons[index].gameObject.SetActive(true);
            _activeWeapon.transform.position = _weaponHoldLocation.transform.position;

            OnActiveWeaponChanged?.Invoke(prevWeaponIndex, index);
        }

        private int GetIndexOfPrevWeapon()
        {
            int index = _activeWeaponIndex - 1;

            if(index < 0)
                index = _weapons.Length - 1;

            while(_weapons[index] == null)
            {
                --index;
                if (index < 0)
                    index = _weapons.Length - 1;
            }

            return index;
        }

        private int GetIndexOfNextWeapon()
        {
            int index = _activeWeaponIndex + 1;

            if (index > _weapons.Length -1)
                index = 0;

            while (_weapons[index] == null)
            {
                ++index;
                if (index > _weapons.Length - 1)
                    index = 0;
            }

            return index;
        }

        private int GetIndexOfWeapon(BaseWeapon weapon)
        {
            int index = -1;

            for(int i=0; i < _weapons.Length;i++)
            {
                if(_weapons[i] == weapon)
                    index = i;
            }

            return index;
        }
    }
}

