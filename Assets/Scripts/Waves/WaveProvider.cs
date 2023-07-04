using Spawning;
using System.Collections.Generic;
using UnityEngine;

namespace Waves
{
    public abstract class WaveProvider : MonoBehaviour
    {
        [SerializeField] protected int _minimumEnemyClusterSize = 2;
        [SerializeField] protected int _maximumEnemyClusterSize = 6;
        [SerializeField] private WaveDifficultySettings _settings;


        protected IEnemySpawnPointProvider _spawnPointProvider;
        protected int _currentWaveIndex = 0;
        protected Wave _currentwave;
        protected List<WaveItem> _waveItems;

        public Wave CurrentWave => _currentwave;
        public WaveDifficultySettings Settings => _settings;  
        
        public List<WaveItem> WaveItems => _waveItems;

        protected virtual void Awake()
        {
            _spawnPointProvider = GetComponent<IEnemySpawnPointProvider>();
        }

        public void CreateNewWave()
        {
            ++_currentWaveIndex;
            _currentwave = new Wave(_currentWaveIndex, _settings);
            Debug.Log("---------------------Creating wave with index : " + _currentWaveIndex);
            _spawnPointProvider.GenerateUsableSpawnPoints();
            GenerateWaveItems();
        }

        protected abstract void GenerateWaveItems();



    }
}

