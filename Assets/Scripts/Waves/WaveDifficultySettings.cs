using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Waves
{
    [System.Serializable]
    public struct EnemySpawnWaveIndex
    {
        [SerializeField] public int WaveIndex;
        [SerializeField] public Enemy Enemy;
    }

    [CreateAssetMenu]
    public class WaveDifficultySettings : ScriptableObject
    {
        [SerializeField] public int AmountOfEnemiesAlliveAtOnce = 15;
        [SerializeField] public int EnemiesAlliveToSpawnMore = 10;
        [SerializeField] public List<EnemySpawnWaveIndex> EnemySpawnList;
        [SerializeField] public AnimationCurve WaveDifficultyCurve;

        public int GetAmountOfWaves()
        {
            return Mathf.RoundToInt(WaveDifficultyCurve.keys[WaveDifficultyCurve.length -1].time);
        }
    }
}

