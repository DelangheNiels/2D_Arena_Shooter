using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu]
    public class EnemySpawnSettings : ScriptableObject
    {
        [SerializeField] public int Strength = 1;
        //Value between 1 and 100 to determine their spawn chance
        [SerializeField,Range(1,100)] public int Weight = 100;
    }
}

