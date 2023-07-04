using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons.Projectiles;

namespace Weapons
{


    [CreateAssetMenu]
    public class BaseWeaponSettings : ScriptableObject
    {
        public int MagazineSize;
        public float ReloadTime;
        public int BulletsPerSecond;
        public BaseProjectile ProjectilePrefab;
    }
}

