using UnityEngine;

namespace schmup
{
    // Konfiguration für eine einzelne Gegnerwelle, als eigenständiges Asset
    // statt fest im Code verankerter Werte. Über "Create > schmup > Wave"
    // im Project-Fenster erstellbar.
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