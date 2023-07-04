using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons.Projectiles
{
    [CreateAssetMenu]
    public class ProjectileSettings : ScriptableObject
    {
        public float MovementSpeed;
        public float Damage;
        public float DespawnTime;
        public LayerMask LayersToHit;
    }
}

