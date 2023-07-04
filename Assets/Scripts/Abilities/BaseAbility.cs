using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public abstract class BaseAbility : MonoBehaviour
    {
        [SerializeField] private float _abilityCooldownTime = 15.0f;

        protected Animator _animator;
        private float _cooldownTimer;
        private bool _canUseAbility = false;

        public bool CanUseAbility => _canUseAbility;

        protected virtual void Awake()
        {
            _animator = transform.root.GetComponentInChildren<Animator>();
        }

        protected virtual void Update()
        {
            if (_canUseAbility)
                return;

            _cooldownTimer += Time.deltaTime;
            if(_cooldownTimer >= _abilityCooldownTime)
            {
                _canUseAbility = true;
                _cooldownTimer = 0;
            }
        }

        //Starts the ability usage animation
        public virtual void PerformAbility()
        {
            if (!_canUseAbility)
                return;

            if(_animator != null)
                _animator.SetBool("UseAbility", true);

            _canUseAbility = false;
        }

        //actual ability 
        public abstract void DoAbility();

    }
}

