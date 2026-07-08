using UnityEngine;

namespace schmup {
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float spawnInterval;
        public float enemySpeed;
        public float enemyFireRate;
        public bool useSineWave;
    }

    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Wave[] waves;
        [SerializeField] float timeBetweenWaves = 5f;

        [Header("Spawn Position Variation")]
        [SerializeField] float minSpawnY = -3f;
        [SerializeField] float maxSpawnY = 3f;

        int currentWave = 0;
        int enemiesSpawned = 0;
        float nextSpawnTime = 0f;
        bool waveComplete = false;
        bool allWavesDone = false;

        void Update()
        {
            if (allWavesDone) return;
            if (currentWave >= waves.Length)
            {
                allWavesDone = true;
                Debug.Log("Alle Wellen fertig → Boss!");
                return;
            }

            Wave wave = waves[currentWave];

            if (enemiesSpawned < wave.enemyCount)
            {
                if (Time.time >= nextSpawnTime)
                {
                    SpawnEnemy(wave);
                    enemiesSpawned++;
                    nextSpawnTime = Time.time + wave.spawnInterval;
                }
            }
            else if (!waveComplete)
            {
                waveComplete = true;
                Invoke(nameof(NextWave), timeBetweenWaves);
            }
        }

        void SpawnEnemy(Wave wave)
        {
            // Zufällige Y-Position innerhalb des Bereichs
            float randomY = Random.Range(minSpawnY, maxSpawnY);
            Vector3 spawnPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y + randomY, spawnPoint.position.z);

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            Enemy e = enemy.GetComponent<Enemy>();
            // Wellen-spezifische Werte setzen
        }

        void NextWave()
        {
            currentWave++;
            enemiesSpawned = 0;
            waveComplete = false;
            Debug.Log($"Welle {currentWave + 1} startet!");
        }
    }
}