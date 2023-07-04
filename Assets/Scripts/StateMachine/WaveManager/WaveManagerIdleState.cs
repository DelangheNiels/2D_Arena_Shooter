using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Waves;

namespace States.WaveManager
{
    public class WaveManagerIdleState : WaveManagerState
    {
        public WaveManagerIdleState(Blackboard blackboard) : base(blackboard)
        {
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
            int waveIndex = ((Wave)_blackboard.GetData("CurrentWave")).Index;
            NotificationManager.CreateNotification("Wave " + waveIndex + " completed!", 5);
        }
    }
}

