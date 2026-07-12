using UnityEngine;

namespace schmup {
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] Transform spawnPoint;
        [SerializeField] WaveData[] waves;
        [SerializeField] float timeBetweenWaves = 5f;

        [Header("Spawn Position Variation")]
        [SerializeField] float minSpawnY = -3f;
        [SerializeField] float maxSpawnY = 3f;

        [Header("Boss")]
        [SerializeField] GameObject bossPrefab;
        [SerializeField] Vector3 bossSpawnOffset = new Vector3(12f, 0f, 0f);
        [SerializeField] float delayBeforeBoss = 3f;

        int currentWave = 0;
        int enemiesSpawned = 0;
        float nextSpawnTime = 0f;
        bool waveComplete = false;
        bool allWavesDone = false;
        bool bossSpawned = false;

        void Start()
        {
            AudioManager.Instance?.PlayLevelMusic();
        }

        void Update()
        {
            if (allWavesDone) return;
            if (currentWave >= waves.Length)
            {
                allWavesDone = true;
                Debug.Log("Alle Wellen fertig → Boss!");
                Invoke(nameof(SpawnBoss), delayBeforeBoss);
                return;
            }

            WaveData wave = waves[currentWave];

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

        void SpawnEnemy(WaveData wave)
        {
            // Zufällige Y-Position innerhalb des Bereichs
            float randomY = Random.Range(minSpawnY, maxSpawnY);
            Vector3 spawnPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y + randomY, spawnPoint.position.z);

            GameObject enemyObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            // Wellen-spezifische Werte aus dem WaveData-Asset könnten hier gesetzt werden,
            // z.B. über public setter methoden auf Enemy, falls gewünscht
        }

        void SpawnBoss()
        {
            if (bossSpawned || bossPrefab == null) return;
            bossSpawned = true;

            Vector3 spawnPos = Camera.main.transform.position + bossSpawnOffset;
            Instantiate(bossPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Boss ist erschienen!");
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