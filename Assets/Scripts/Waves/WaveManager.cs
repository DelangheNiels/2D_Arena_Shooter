using BehaviourTree;
using Spawning;
using States;
using States.WaveManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Waves
{
    public class WaveManager : MonoBehaviour
    {
        private WaveProvider _waveProvider;
        private StateMachine _waveManagerStateMachine;
        private BossProvider _bossProvider;

        private Blackboard _blackboard;

        private WaveManagerCreateWaveState _createWaveState;
        private WaveManagerWaitState _waitState;
        private WaveManagerSpawningState _spawnState;
        private WaveManagerIdleState _idleState;
        private WaveManagerBossState _bossState;

        private bool _hasSpawnedBoss = false;

        private void Awake()
        {
            _waveProvider = GetComponent<WaveProvider>();
            _bossProvider = GetComponent<BossProvider>();

            _blackboard = new Blackboard();
            InitializeBlackboard();

            _createWaveState = new WaveManagerCreateWaveState(_blackboard, _waveProvider);
            _waitState = new WaveManagerWaitState(_blackboard, 5, this);
            _spawnState = new WaveManagerSpawningState(_blackboard, _waveProvider);
            _idleState = new WaveManagerIdleState(_blackboard);
            _bossState = new WaveManagerBossState(_blackboard, _bossProvider);

            _waveManagerStateMachine = new StateMachine();
            _waveManagerStateMachine.SetInitialState(_waitState);

            _waveManagerStateMachine.AddTransition(_waitState, _createWaveState,IsDoneWaiting);
            _waveManagerStateMachine.AddTransition(_createWaveState, _spawnState, IsWaveInitialized);
            _waveManagerStateMachine.AddTransition(_spawnState, _idleState, HaveAllEnemiesSpawned);
            _waveManagerStateMachine.AddTransition(_idleState, _waitState,CanStartNextWave);
            _waveManagerStateMachine.AddTransition(_idleState, _bossState, ShouldSpawnBoss);
        }

        private void Update()
        {
            _waveManagerStateMachine.Update();
        }

        private void InitializeBlackboard()
        {
            _blackboard.SetData("IsWaveInitialized", false);
            _blackboard.SetData("ShouldWait", true);
            _blackboard.SetData("CurrentWave", null);
            _blackboard.SetData("HaveAllEnemiesSpawned", false);
        }

        private bool IsDoneWaiting()
        {
            return !(bool)_blackboard.GetData("ShouldWait");
        }

        private bool IsWaveInitialized()
        {
            return (bool)_blackboard.GetData("IsWaveInitialized") && _blackboard.GetData("CurrentWave") != null;
        }

        private bool HaveAllEnemiesSpawned()
        {
            return (bool)_blackboard.GetData("HaveAllEnemiesSpawned");
        }

        private bool CanStartNextWave()
        {
            return AreAllEnemiesDead() && HasNextWave(); 
        }

        private bool AreAllEnemiesDead()
        {
            Wave wave = (Wave)_blackboard.GetData("CurrentWave");

            if (wave == null)
                return false;

            if(wave.TotalAmountOfEnemies == wave.AmountOfEnemiesDied)
                return true;
            else
                return false;
        }

        private bool HasNextWave()
        {
            if(_waveProvider.CurrentWave.Index < _waveProvider.Settings.GetAmountOfWaves())
                return true;

            return false;
        }

        private bool ShouldSpawnBoss()
        {
            if (_waveProvider == null || _waveProvider.CurrentWave.Index < _waveProvider.Settings.GetAmountOfWaves() || _hasSpawnedBoss || !AreAllEnemiesDead())
                return false;

            _hasSpawnedBoss = true;
            return true;
        }
    }
}

