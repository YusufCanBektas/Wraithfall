using UnityEngine;

namespace schmup {
    // Steuert den Ablauf eines Levels: spawnt Gegnerwellen nacheinander
    // und lässt nach der letzten Welle den Boss erscheinen.
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

        [Header("Item-Drops")]
        [SerializeField] int itemDropsStartFromWave = 1; // 1 = ab erster Welle, 2 = ab zweiter Welle, usw.

        [Header("Musik")]
        [SerializeField] bool isLevel2 = false; // bestimmt, welche Level-Musik gestartet wird

        int currentWave = 0;
        int enemiesSpawned = 0;
        float nextSpawnTime = 0f;
        bool waveComplete = false;
        bool allWavesDone = false;
        bool bossSpawned = false;

        void Start()
        {
            if (isLevel2)
                AudioManager.Instance?.RestartLevel2Music();
            else
                AudioManager.Instance?.RestartLevelMusic();
        }

        void Update()
        {
            if (allWavesDone) return;

            // Verhindert, dass nach Spielende (Tod/Sieg) noch neue Wellen oder der Boss spawnen
            if (GameManager.Instance != null && GameManager.Instance.IsGameEnded()) return;

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

            // Wellenzählung ist 0-basiert intern (currentWave), aber "Welle 1" für den Menschen ist currentWave == 0
            bool dropsAllowed = (currentWave + 1) >= itemDropsStartFromWave;
            enemy?.SetItemDropsEnabled(dropsAllowed);

            // Bewegungsmuster aus dem WaveData-Asset übernehmen (z.B. Sinuswelle ab bestimmter Welle)
            enemy?.SetMovementPattern(wave.useSineWave, wave.enemySpeed > 0 ? wave.enemySpeed : 3f);
        }

        void SpawnBoss()
        {
            if (bossSpawned || bossPrefab == null) return;
            if (GameManager.Instance != null && GameManager.Instance.IsGameEnded()) return;
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

        public int GetCurrentWaveNumber() => currentWave + 1; // 1-basiert für Menschen
    }
}