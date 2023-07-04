using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.AI
{
    public class UseAbility : Node
    {
        private Transform _transform;
        private Enemy _enemy;

        public UseAbility(Blackboard blackboard, Transform transform) : base(blackboard)
        {
            _transform = transform;
            _enemy = _transform.GetComponent<Enemy>();
        }

        public override NodeState Evaluate()
        {
            if(_enemy.Ability == null || !_enemy.Ability.CanUseAbility)
            {
                _state = NodeState.FAILED;
                return _state;
            }
            else
            {
                _state = NodeState.SUCCESS;
                _enemy.Ability.PerformAbility();
                return _state;
            }
        }
    }
}

