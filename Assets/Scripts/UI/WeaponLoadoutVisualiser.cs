using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WeaponLoadoutVisualiser : MonoBehaviour
    {
        [SerializeField] WeaponInfoVisualiser _weaponInfoPrefab;
        [SerializeField] WeaponLoadout _weaponLoadout;
        [SerializeField] GameObject _weaponLoadoutPanel;

        private List<WeaponInfoVisualiser> _visualisers = new List<WeaponInfoVisualiser>();
        // Start is called before the first frame update
        void Start()
        {
            ShowWeaponList();
            _weaponLoadout.OnActiveWeaponChanged += HandleActiveWeaponChanged;
        }

        private void OnDestroy()
        {
            _weaponLoadout.OnActiveWeaponChanged -= HandleActiveWeaponChanged;
        }

        private void ShowWeaponList()
        {
            int amountOfweapons = _weaponLoadout.Weapons.Length;
            for(int i=0; i < amountOfweapons; i++)
            {
                var weaponInfo = Instantiate(_weaponInfoPrefab);
                weaponInfo.transform.parent = _weaponLoadoutPanel.transform;
                weaponInfo.SetWeapon(_weaponLoadout.Weapons[i]);
                weaponInfo.SetActiveInActive(false);

                _visualisers.Add(weaponInfo);

                if (i == 0)
                    weaponInfo.SetActiveInActive(true);
            }
        }

        private void HandleActiveWeaponChanged(int prevIndex, int activeWeaponIndex)
        {
            _visualisers[prevIndex].SetActiveInActive(false);
            _visualisers[activeWeaponIndex].SetActiveInActive(true);
        }
    }
}

