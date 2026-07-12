using UnityEngine;

namespace schmup
{
    [CreateAssetMenu(fileName = "Wave_", menuName = "schmup/Wave")]
    public class WaveData : ScriptableObject
    {
        public int enemyCount;
        public float spawnInterval;
        public float enemySpeed;
        public float enemyFireRate;
        public bool useSineWave;
    }
}
