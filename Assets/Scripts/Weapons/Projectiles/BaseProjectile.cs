using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class BaseProjectile : MonoBehaviour
    {

        [SerializeField] private ProjectileSettings _projectileSettings;

        private Vector3 _movementVector;
        private float _despawnTimer = 0.0f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //move bullet
            transform.position += _movementVector * _projectileSettings.MovementSpeed * Time.deltaTime;

            //Despawn bullet
            _despawnTimer += Time.deltaTime;
            if(_despawnTimer >= _projectileSettings.DespawnTime)
                Destroy(gameObject);
        }

        public void SetMovementVector(Vector2 movementVector)
        {
            _movementVector = movementVector;
            _movementVector.z = 0.0f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var damageable = collision.transform.root.GetComponentInChildren<Damageable>();

            if (damageable != null)
                damageable.DealDamage(_projectileSettings.Damage);

            Destroy(gameObject);
        }
    }
}

