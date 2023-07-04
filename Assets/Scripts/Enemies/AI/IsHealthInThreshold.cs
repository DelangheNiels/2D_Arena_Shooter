using BehaviourTree;
using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.AI
{
    public class IsHealthInThreshold : Node
    {
        private HealthComponent _healthComponent;
        private float _maxHealthPercentage;
        private float _minHealthPercentage;

        public IsHealthInThreshold(Blackboard blackboard, HealthComponent healthComponent,float maxHealthPercentage, float minHealthPercentage) : base(blackboard)
        {
            _healthComponent = healthComponent;
            _maxHealthPercentage = maxHealthPercentage;
            _minHealthPercentage = minHealthPercentage;
        }

        public override NodeState Evaluate()
        {
            if(_healthComponent.GetHealthPercentage() >= _minHealthPercentage && _healthComponent.GetHealthPercentage() <= _maxHealthPercentage)
            {
                _state = NodeState.SUCCESS;
                return _state;
            }
            else
            {
                _state = NodeState.FAILED;
                return _state;
            }
        }
    }
}

