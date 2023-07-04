using BehaviourTree;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.AI
{
    public class FindPlayerPosition : Node
    {
        private GameObject _player;
        private Transform _transform;

        public FindPlayerPosition(Blackboard blackboard, Transform transform) : base(blackboard)
        {
            _transform = transform; 
        }

        public override NodeState Evaluate()
        {
            if(_player == null)
            {
                _player = Transform.FindObjectOfType<PlayerManager>().gameObject;
            }

            if (_player == null)
            {
                _state = NodeState.FAILED;
                return NodeState.FAILED;
            }


            _blackboard.SetData("TargetPosition", _player.transform);

            return NodeState.SUCCESS;
        }
    }
}

