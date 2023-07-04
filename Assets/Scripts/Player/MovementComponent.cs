using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 5.0f;
        [SerializeField] private float _movementOffset = 0.05f;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private ContactFilter2D _movementFilter;

        private Vector2 _movementVector = Vector2.zero;
        private List<RaycastHit2D> _hitColliders = new List<RaycastHit2D>();

        private bool _canMove = true;

        private Animator _animator;
        private const string ANIM_MOVE = "IsMoving";

        private HealthComponent _healtComponent;

        private void Start()
        {
            _animator = transform.root.GetComponentInChildren<Animator>();

            _healtComponent = transform.root.GetComponentInChildren<HealthComponent>();

            if(_healtComponent != null)
                _healtComponent.OnDied += HandleDied;

        }

        private void FixedUpdate()
        {
            //Check movement
            if(TryMove(_movementVector) == false)
            {
                bool couldMove = false;
                //Try moving vertically
                couldMove = TryMove(new Vector2(0, _movementVector.y));

                if (!couldMove)
                    //Try moving horizontally
                    TryMove(new Vector2(_movementVector.x, 0));
            }
        }

        private bool TryMove(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return false;

            int colliders = _rigidbody.Cast(direction, _movementFilter, _hitColliders, _movementSpeed * Time.fixedDeltaTime * _movementOffset);

            if (colliders != 0)
                return false;

            _rigidbody.MovePosition(_rigidbody.position + direction * _movementSpeed * Time.fixedDeltaTime);
            return true;
        }

        public void Move(Vector2 movementVector)
        {
            if(!_canMove)
            {
                _movementVector = Vector2.zero;
                return;
            }

            _movementVector = movementVector;

            if(_animator == null)
                return;

            if (movementVector != Vector2.zero)
                _animator.SetBool(ANIM_MOVE, true);
            else
                _animator.SetBool(ANIM_MOVE, false);

        }

        private void HandleDied(HealthComponent healthComp)
        {
            healthComp.OnDied -= HandleDied;
            _canMove = false;
        }
    }

}

