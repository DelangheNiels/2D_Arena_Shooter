using BehaviourTree;
using UI;
using UnityEngine;
using Waves;

namespace States.WaveManager
{
    public class WaveManagerCreateWaveState : WaveManagerState
    {
        private WaveProvider _waveProvider;

        public WaveManagerCreateWaveState(Blackboard blackboard, WaveProvider waveProvider) : base(blackboard)
        {
           _waveProvider = waveProvider;
        }

        public override void OnEnter()
        {
            _waveProvider.CreateNewWave();
            _blackboard.SetData("CurrentWave", _waveProvider.CurrentWave);
            _blackboard.SetData("IsWaveInitialized", true);

            NotificationManager.CreateNotification("Wave " + _waveProvider.CurrentWave.Index + " started!", 5);
        }

        public override void OnExit()
        {
            _blackboard.SetData("IsWaveInitialized", false);
        }
    }

}
