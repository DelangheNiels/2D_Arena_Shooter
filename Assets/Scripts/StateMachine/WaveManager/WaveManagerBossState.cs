using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Waves;

namespace States.WaveManager
{
    public class WaveManagerBossState : WaveManagerState
    {
        BossProvider _bossProvider;

        public WaveManagerBossState(Blackboard blackboard, BossProvider bossProvider) : base(blackboard)
        {
            _bossProvider = bossProvider;
        }

        public override void OnEnter()
        {
            _bossProvider.SpawnBoss();
        }

        public override void OnExit()
        {
        }
    }
}

