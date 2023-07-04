using UnityEngine;

namespace Enemies.Attack
{
    [CreateAssetMenu]
    public class EnemyAttackSettings : ScriptableObject
    {
        //time between attacks
        public float AttackSpeed = 1.0f;
        public float MinimumAttackRange = 0.2f;
        public float MaximumAttackRange = 0.2f;
    }
}