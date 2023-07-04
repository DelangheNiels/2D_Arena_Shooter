using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States.WaveManager
{
    public abstract class WaveManagerState : State
    {
        protected Blackboard _blackboard;
        public WaveManagerState (Blackboard blackboard)
        {
            _blackboard = blackboard;
        }
        public abstract override void OnEnter();

        public abstract override void OnExit();

    }
}
