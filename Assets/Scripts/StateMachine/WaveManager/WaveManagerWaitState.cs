using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States.WaveManager
{
    public class WaveManagerWaitState : WaveManagerState
    {
        private int _waitTime = 0;
        private Waves.WaveManager _waveManager;

       public WaveManagerWaitState(Blackboard blackboard, int waitTime, Waves.WaveManager waveManager) : base(blackboard)
        {
            _waitTime = waitTime;
            _waveManager = waveManager;
        }

        public override void OnEnter()
        {
            _waveManager.StartCoroutine(CoWaitForSeconds(_waitTime));
        }

        public override void OnExit()
        {
            _waveManager.StopAllCoroutines();
            _blackboard.SetData("ShouldWait", true);

        }

        private IEnumerator CoWaitForSeconds(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            _blackboard.SetData("ShouldWait",false);
        }
    }
}

