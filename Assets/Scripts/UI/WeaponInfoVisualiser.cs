using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class WeaponInfoVisualiser : MonoBehaviour
    {
        [SerializeField] private Image _weaponImageObject;
        [SerializeField] private Image _weaponActiveObject;
        [SerializeField] private TMP_Text _ammoTextObject;

        private BaseWeapon _weapon;
       
        private void OnDestroy()
        {
            if(_weapon == null)
                return;

            _weapon.OnAmmoAmountChanged -= HandleWeaponAmmoAmountChanged;
        }

        public void SetWeapon(BaseWeapon weapon)
        {
            _weapon = weapon;
            _weapon.OnAmmoAmountChanged += HandleWeaponAmmoAmountChanged;
            _weaponImageObject.sprite = _weapon.WeaponImage;
            _ammoTextObject.text = _weapon.MaxMagazineSize.ToString();
        }

        public void SetActiveInActive(bool active)
        {
            _weaponActiveObject.gameObject.SetActive(!active);
        }

        private void HandleWeaponAmmoAmountChanged()
        {
            _ammoTextObject.text = _weapon.CurrentAmountInMagazine.ToString();

            if(_weapon.CurrentAmountInMagazine <= 0)
                _ammoTextObject.color = Color.red;
            else
                _ammoTextObject.color= Color.white;
        }
    }
}

