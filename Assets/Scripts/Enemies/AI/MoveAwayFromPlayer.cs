using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.AI
{
    public class MoveAwayFromPlayer : Node
    {
        private Transform _transform;
        private Enemy _enemy;

        public MoveAwayFromPlayer(Blackboard blackboard, Transform transform) : base(blackboard)
        {
            _transform = transform;
            _enemy = _transform.GetComponent<Enemy>();
        }

        public override NodeState Evaluate()
        {
            if (_blackboard.GetData("TargetPosition") == null)
            {
                _state = NodeState.FAILED;
                return _state;
            }

            Vector2 targetPosition = ((Transform)_blackboard.GetData("TargetPosition")).position;

            float distance = Vector2.Distance(targetPosition, _transform.position);

            if (distance < _enemy.EnemyAttack.Settings.MaximumAttackRange)
            {
                Vector2 direction = ((Vector2)_transform.position - targetPosition).normalized;
                _enemy.MovementComponent.Move(direction);
                _state = NodeState.SUCCESS;
                return _state;
            }
            else
            {
                //_enemy.MovementComponent.Move(Vector2.zero);
                _state = NodeState.FAILED;
                return _state;
            }
        }
    }
}

