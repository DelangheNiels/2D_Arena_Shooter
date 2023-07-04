using Enemies;
using UnityEngine;

namespace Animations
{
    public class AnimationEventHandler : MonoBehaviour
    {
        private Enemy _enemy;

        private void Start()
        {
            _enemy = transform.root.GetComponentInChildren<Enemy>();
        }

        public void DestroyObject()
        {
            Destroy(transform.root.gameObject);
        }

        public void PerformAttack()
        {
            if (_enemy.EnemyAttack == null)
                return;

            _enemy.EnemyAttack.SpawnAttack();
        }

        public void EnabeMovement()
        {
            _enemy.MovementComponent.gameObject.SetActive(true);
        }

        public void DisableMovement()
        {
            _enemy.MovementComponent.gameObject.SetActive(false);
        }

        public void UseAbility()
        {
            if (_enemy.Ability == null)
                return;

            _enemy.Ability.DoAbility();
        }
    }
}

