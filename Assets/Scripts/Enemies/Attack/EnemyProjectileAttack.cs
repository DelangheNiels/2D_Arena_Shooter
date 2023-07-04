using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Attack
{
    public class EnemyProjectileAttack : BaseEnemyAttack
    {
        [SerializeField] private float _movementSpeed = 3.0f;

        private Vector3 _moveVector = Vector2.zero;
        // Start is called before the first frame update
        void Start()
        {
            transform.SetParent(null);
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            transform.position += _moveVector * _movementSpeed * Time.deltaTime;
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.tag != "Player")
                return;

            Destroy(gameObject);
        }

        public void SetMovementVector(Vector2 moveVector)
        {
            _moveVector = moveVector;
            _moveVector.Normalize();

            float angle = Mathf.Atan2(_moveVector.y, _moveVector.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
        }
    }
}

