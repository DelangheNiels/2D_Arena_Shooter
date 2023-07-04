using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Enemies
{
    public class BossEnemy : Enemy
    {
        [SerializeField] private string _name;

        private BossHealthbar _healthbar;

        public string Name => _name;

        protected override void Awake()
        {
            base.Awake();

            _healthbar = FindObjectOfType<BossHealthbar>();

            if(_healthbar != null)
                _healthbar.Initialize(_name, _healthComponent);

        }
    }
}

